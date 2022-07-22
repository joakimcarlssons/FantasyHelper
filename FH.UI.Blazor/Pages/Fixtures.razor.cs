namespace FH.UI.Blazor.Pages
{
    public record FixtureDisplayModel(TeamViewModel Team, List<List<FixtureViewModel>> Fixtures);

    public partial class Fixtures : IDisposable
    {
        #region Injections

        [Inject]
        public IConfiguration Configuration { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public StateContainer StateContainer { get; set; }

        #endregion

        #region Properties

        public List<FixtureDisplayModel> TeamFixtures { get; set; } = new();
        public IEnumerable<Team> Teams { get; set; }
        public IEnumerable<Gameweek> Gameweeks { get; set; }

        public int GameweeksToDisplay { get; set; } = 10;
        public int NextGameweek => Gameweeks?.FirstOrDefault(gw => gw.IsNext)?.GameweekId ?? 1;

        private string SelectedFantasyGame { get; set; }
        private string Config { get; set; }

        #endregion

        protected override void OnInitialized()
        {
            StateContainer.OnChange += StateHasChanged;
        }

        public void Dispose()
        {
            StateContainer.OnChange -= StateHasChanged;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // If the selected game has been updated
            if (StateContainer.SelectedFantasyGame != SelectedFantasyGame || firstRender)
            {
                try
                {
                    StateContainer.DataIsLoading = true;

                    // Update selected game
                    SelectedFantasyGame = StateContainer.SelectedFantasyGame;

                    // Update configs
                    SetConfigs();

                    // Load data
                    await LoadGameweeks();
                    await LoadTeams();

                    TeamFixtures.Clear();
                    foreach (var team in Teams)
                    {
                        await LoadFixturesForTeam(team);
                    }

                    StateHasChanged();
                }
                finally
                {
                    StateContainer.DataIsLoading = false;
                }
            }
        }

        private void SetConfigs()
        {
            switch (StateContainer.SelectedFantasyGame)
            {
                case "FPL":
                    Config = "FPL";
                    break;
                case "Fantasy Allsvenskan":
                    Config = "FantasyAllsvenskan";
                    break;
            }
        }

        private async Task LoadGameweeks()
        {
            var response = await HttpClient.GetAsync(Configuration[$"{ Config }:Gameweeks"]);
            if (response.IsSuccessStatusCode)
            {
                var gameweeks = JsonSerializer.Deserialize<IEnumerable<Gameweek>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (gameweeks == null)
                {
                    Console.WriteLine("Could not parse gameweeks");
                    throw new NullReferenceException(nameof(gameweeks));
                }

                Gameweeks = gameweeks;
            }
        }

        private async Task LoadTeams()
        {
            var response = await HttpClient.GetAsync(Configuration[$"{ Config }:Teams"]);
            if (response.IsSuccessStatusCode)
            {
                var teams = JsonSerializer.Deserialize<IEnumerable<Team>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (teams == null)
                {
                    Console.WriteLine("Could not parse teams");
                    throw new NullReferenceException(nameof(teams));
                }

                Teams = Mapper.Map<IEnumerable<Team>>(teams);
            }
        }

        private async Task LoadFixturesForTeam(Team team)
        {
            var response = await HttpClient.GetAsync($"{ Configuration[$"{ Config }:Fixtures"] }/team/{ team.TeamId }");
            if (response.IsSuccessStatusCode)
            {
                var fixtures = JsonSerializer.Deserialize<IEnumerable<Fixture>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (fixtures == null)
                {
                    Console.WriteLine("Could not parse fixtures");
                    throw new NullReferenceException(nameof(fixtures));
                }

                // Load double gameweeks
                var groupedFixtures = new List<List<Fixture>>();
                var dbgw = fixtures.GroupBy(f => f.GameweekId).Where(f => f.Skip(1).Any()).SelectMany(f => f.Select(fixture => fixture)).ToList();
                if (dbgw.Any())
                {
                    groupedFixtures.Add(dbgw);
                }

                // Load single gameweeks
                fixtures.ToList().ForEach(fixture =>
                {
                    // Don't load already loaded double gameweek fixtures
                    if (!groupedFixtures.Where(grp => grp.Contains(fixture)).Any())
                    {
                        if (fixtures != null) groupedFixtures.Add(new() { fixture });
                    }
                });

                // Order fixtures by gameweek
                groupedFixtures = groupedFixtures.OrderBy(f => f.FirstOrDefault()?.GameweekId).ToList();

                // Add new display model
                TeamFixtures.Add(new(
                    Mapper.Map<TeamViewModel>(team),
                    Mapper.Map<List<List<FixtureViewModel>>>(groupedFixtures)));
            }
        }
    }
}
