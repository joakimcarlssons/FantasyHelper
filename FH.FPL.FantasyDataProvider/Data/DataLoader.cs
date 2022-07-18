using Microsoft.Extensions.Options;

namespace FantasyHelper.FPL.DataProvider.Data
{
    public class DataLoader : IDataLoader
    {
        #region Private Members

        private readonly HttpClient _httpClient;
        private readonly FPLOptions _fplConfig;
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _scopeFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DataLoader(HttpClient httpClient, IOptions<FPLOptions> fplConfig, IMapper mapper, IServiceScopeFactory scopeFactory)
        {
            _httpClient = httpClient;
            _fplConfig = fplConfig.Value;
            _mapper = mapper;
            _scopeFactory = scopeFactory;
        }

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

        #endregion

        public async Task<IEnumerable<ExternalFixtureDto>> LoadFixtureData()
        {
            try
            {
                var response = await _httpClient.GetAsync(_fplConfig.FixturesEndpoint);
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
                throw;
            }
        }

        public async Task<ExternalRootDto> LoadRootData()
        {
            try
            {
                var response = await _httpClient.GetAsync(_fplConfig.RootEndpoint);
                if (response.IsSuccessStatusCode)
                {
                    var data = JsonSerializer.Deserialize<ExternalRootDto>(await response.Content.ReadAsStringAsync());
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
                throw;
            }
        }
    }
}
