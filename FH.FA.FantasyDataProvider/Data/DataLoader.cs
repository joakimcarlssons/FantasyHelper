﻿namespace FH.FA.FantasyDataProvider.Data
{
    public class DataLoader : IDataLoader
    {
        #region Private Member

        private readonly HttpClient _httpClient;
        private readonly FAOptions _faConfig;
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMessageBusPublisher _messagePublisher;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DataLoader(HttpClient httpClient, IOptions<FAOptions> faConfig, IMapper mapper, IServiceScopeFactory scopeFactory, IMessageBusPublisher messagePublisher)
        {
            _httpClient = httpClient;
            _faConfig = faConfig.Value;
            _mapper = mapper;
            _scopeFactory = scopeFactory;
            _messagePublisher = messagePublisher;
        }

        #endregion

        /// <summary>
        /// Loads root data including teams, players and gameweeks from Fantasy Allsvenskan API
        /// </summary>
        public async Task<ExternalRootDto> LoadRootData()
        {
            try
            {
                var response = await _httpClient.GetAsync(_faConfig.RootEndpoint);
                if (response.IsSuccessStatusCode)
                {
                    var data = JsonSerializer.Deserialize<ExternalRootDto>(await response.Content.ReadAsStringAsync());
                    if (data == null) throw new NullReferenceException(nameof(ExternalRootDto));

                    return data;
                }
                else
                {
                    throw new HttpRequestException();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> { ex.Message }");
                throw;
            }
        }

        public void PublishPlayers(IDataProviderRepository repo)
        {
            var players = repo.GetAllPlayers();
            _messagePublisher.PublishData(_mapper.Map<IEnumerable<PlayerReadDto>>(players).ToPublishDataDto(EventType.PlayersPublished, EventSource.FantasyAllsvenskan));
        }

        public void PublishTeams(IDataProviderRepository repo)
        {
            var teams = repo.GetAllTeams();
            _messagePublisher.PublishData(_mapper.Map<IEnumerable<TeamReadDto>>(teams).ToPublishDataDto(EventType.TeamsPublished, EventSource.FantasyAllsvenskan));
        }

        /// <summary>
        /// Saves loaded data to database
        /// </summary>
        public async Task<bool> SaveToDatabase()
        {
            try
            {
                // Get the repository service
                using var scope = _scopeFactory.CreateScope();
                var repo = scope.ServiceProvider.GetRequiredService<IDataProviderRepository>();
                
                // Load data
                var rootData = await LoadRootData();

                // Save data to database
                rootData.Players.ForEach(player => repo.SavePlayer(_mapper.Map<Player>(player)));
                rootData.Teams.ForEach(team => repo.SaveTeam(_mapper.Map<Team>(team)));
                rootData.Gameweeks.ForEach(gw => repo.SaveGameweek(_mapper.Map<Gameweek>(gw)));
                var dbResult = repo.SaveChanges();

                // Publish events with loaded data
                if (dbResult)
                {
                    PublishPlayers(repo);
                    PublishTeams(repo);
                }

                // Return result
                return dbResult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
