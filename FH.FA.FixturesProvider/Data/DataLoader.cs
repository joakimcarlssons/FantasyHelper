using HtmlAgilityPack;

namespace FH.FA.FixturesProvider.Data
{
    public class DataLoader : IDataLoader
    {
        #region Private Members

        private readonly HttpClient _httpClient;
        private readonly EndpointOptions _endpoints;
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _scopeFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DataLoader(HttpClient httpClient, IOptions<EndpointOptions> endpoints, IMapper mapper, IServiceScopeFactory scopeFactory)
        {
            _httpClient = httpClient;
            _endpoints = endpoints.Value;
            _mapper = mapper;
            _scopeFactory = scopeFactory;
        }

        #endregion

        public async Task<bool> LoadFixtureData()
        {
            try
            {
                // Get repository instance
                using var scope = _scopeFactory.CreateScope();
                var repo = scope.ServiceProvider.GetService<IRepository>();

                // Get data
                var response = await _httpClient.GetAsync(_endpoints.FixturesEndpoint);
                if (response.IsSuccessStatusCode)
                {
                    var str = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<IEnumerable<ExternalFixtureDto>>(await response.Content.ReadAsStringAsync());
                    if (data == null) throw new NullReferenceException(nameof(IEnumerable<ExternalFixtureDto>));

                    data.ToList().ForEach(fixture => repo.SaveFixture(_mapper.Map<Fixture>(fixture)));
                    return repo.SaveChanges();
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

        public async Task<bool> LoadLeagueData()
        {
            try
            {
                // Get repository instance
                using var scope = _scopeFactory.CreateScope();
                var repo = scope.ServiceProvider.GetService<IRepository>();

                // Get data
                var response = await _httpClient.GetAsync(_endpoints.LeagueEndpoint);
                if (response.IsSuccessStatusCode)
                {
                    // Parse HTML response
                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(await response.Content.ReadAsStringAsync());

                    // Get teams
                    var teams = htmlDoc.DocumentNode.Descendants("table")
                        .FirstOrDefault(node => node.GetAttributeValue("class", "")
                        .Contains("full-league-table"))
                        .Descendants("tbody")
                        .FirstOrDefault()
                        .Descendants("tr")
                        .ToList();

                    // Parse each team and update it in the database
                    foreach (var teamNode in teams)
                    {
                        // Get existing names from database
                        var existingNames = repo.GetAllTeams().Select(team => team.Name);

                        // Get team name of extracted team
                        var teamName = teamNode.Descendants("td")
                            .FirstOrDefault(node => node.GetAttributeValue("class", "").Contains("team"))
                            .Descendants("a").FirstOrDefault().InnerText;

                        // Get team from database that matches the name
                        var team = repo.GetTeam(team => team.Name == existingNames.FirstOrDefault(tn => teamName.Contains(tn)));
                        if (team == null)
                        {
                            Console.WriteLine($"--> Could not find team with name { teamName }");
                            throw new NullReferenceException(nameof(Team));
                        }

                        // Map remaining properties
                        team.Position = int.Parse(teamNode.Descendants("td")
                            .FirstOrDefault(node => node.GetAttributeValue("class", "").Contains("position"))
                            .Descendants("span").FirstOrDefault().InnerText);
                        team.MatchesPlayed = int.Parse(teamNode.SelectSingleNode(".//td[@class='mp']").InnerText);
                        team.Wins = int.Parse(teamNode.SelectSingleNode(".//td[@class='win']").InnerText);
                        team.Draws = int.Parse(teamNode.SelectSingleNode(".//td[@class='draw']").InnerText);
                        team.Losses = int.Parse(teamNode.SelectSingleNode(".//td[@class='loss']").InnerText);
                        team.GoalsScored = int.Parse(teamNode.SelectSingleNode(".//td[@class='gf']").InnerText);
                        team.GoalsConceded = int.Parse(teamNode.SelectSingleNode(".//td[@class='ga']").InnerText);
                        team.GoalDifference = int.Parse(teamNode.SelectSingleNode(".//td[@class='gd']").InnerText);
                        team.Points = int.Parse(teamNode.Descendants("td").FirstOrDefault(node => node.GetAttributeValue("class", "").Contains("points")).InnerText);

                        // Save team in database
                        repo.SaveTeam(team);
                    }
                }
                else
                {
                    throw new HttpRequestException();
                }

                return repo.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> { ex.Message }");
                throw;
            }
        }

        public bool UpdateFixtureDifficulties()
        {
            // POINTS CALCULATION:
            // Team playing at home => 1 point
            // Team playing away => 2 points
            // Opponent is top 5 => 3 points
            // Team with better placing => 1 point
            // Team with worse placing => 2 points
            //      If placing is > 4 places => 3 points
            //      If placing is > 8 places => Team leading -1 point

            try
            {
                // Get repository instance
                using var scope = _scopeFactory.CreateScope();
                var repo = scope.ServiceProvider.GetService<IRepository>();

                // Get fixtures
                var fixtures = repo.GetAllFixtures();

                foreach (var fixture in fixtures)
                {
                    // Make sure we start from 0
                    fixture.AwayTeamDifficulty = fixture.HomeTeamDifficulty = 0;

                    // Get teams
                    var homeTeam = repo.GetTeam(team => team.TeamId == fixture.HomeTeamId);
                    var awayTeam = repo.GetTeam(team => team.TeamId == fixture.AwayTeamId);

                    // Team playing at home => 1 point
                    // Team playing away => 2 points
                    fixture.AwayTeamDifficulty += 1;
                    fixture.HomeTeamDifficulty += 2;

                    // Opponent is top 5 => 3 points
                    if (homeTeam.Position <= 5) fixture.HomeTeamDifficulty += 3;
                    if (awayTeam.Position <= 5) fixture.AwayTeamDifficulty += 3;

                    // Team with better placing => 1 point
                    // Team with worse placing => 2 points
                    if (awayTeam.Position > homeTeam.Position)
                    {
                        fixture.AwayTeamDifficulty += 1;

                        // If placing is > 4 places => 3 points
                        if ((awayTeam.Position - homeTeam.Position) > 4)
                        {
                            fixture.HomeTeamDifficulty += 3;
                        }
                        else
                        {
                            fixture.HomeTeamDifficulty += 2;
                        }

                        // If placing is > 8 places => Team leading -1 point
                        if ((awayTeam.Position - homeTeam.Position) > 8)
                        {
                            fixture.AwayTeamDifficulty -= 1;
                        }
                    }
                    else
                    {
                        fixture.HomeTeamDifficulty += 1;

                        // If placing is > 4 places => 3 points
                        if ((homeTeam.Position - awayTeam.Position) > 4)
                        {
                            fixture.AwayTeamDifficulty += 3;
                        }  
                        else
                        {
                            fixture.AwayTeamDifficulty += 2;
                        }

                        // If placing is > 8 places => Team leading -1 point
                        if ((homeTeam.Position - awayTeam.Position) > 8)
                        {
                            fixture.HomeTeamDifficulty -= 1;
                        }
                    }

                    // Min points are 2
                    if (fixture.HomeTeamDifficulty < 2) fixture.HomeTeamDifficulty = 2;
                    if (fixture.AwayTeamDifficulty < 2) fixture.AwayTeamDifficulty = 2;

                    // Max points are 5
                    if (fixture.HomeTeamDifficulty > 5) fixture.HomeTeamDifficulty = 5;
                    if (fixture.AwayTeamDifficulty > 5) fixture.AwayTeamDifficulty = 5;

                    repo.SaveFixture(fixture);
                }

                return repo.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> { ex.Message }");
                throw;
            }
        }
    }
}
