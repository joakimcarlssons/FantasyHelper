namespace FantasyHelper.FA.DataProvider.Data
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
        /// Loads fixture data from the Fantast Allsvenskan API
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ExternalFixtureDto>> LoadFixtureData()
        {
            try
            {
                var response = await _httpClient.GetAsync(_faConfig.FixturesEndpoint);
                if (response.IsSuccessStatusCode)
                {
                    var data = JsonSerializer.Deserialize<IEnumerable<ExternalFixtureDto>>(await response.Content.ReadAsStringAsync());
                    if (data == null) throw new NullReferenceException(nameof(data));

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
                    if (data == null) throw new NullReferenceException(nameof(data));

                    // Publish events with loaded data
                    var teamsPublishedTeamDtos = _mapper.Map<IEnumerable<TeamsPublishedTeamDto>>(data.Teams);
                    var publishedTeamsDto = new TeamsPublishedDto
                    {
                        Event = "Teams_Published",
                        Teams = teamsPublishedTeamDtos
                    };

                    _messagePublisher.PublishTeams(publishedTeamsDto);

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
                var fixtureData = await LoadFixtureData();

                // Save data to database
                rootData.Players.ForEach(player => repo.SavePlayer(_mapper.Map<Player>(player)));
                rootData.Teams.ForEach(team => repo.SaveTeam(_mapper.Map<Team>(team)));
                rootData.Gameweeks.ForEach(gw => repo.SaveGameweek(_mapper.Map<Gameweek>(gw)));
                fixtureData.ToList().ForEach(fixture => repo.SaveFixture(_mapper.Map<Fixture>(fixture)));

                // Return result
                return repo.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
