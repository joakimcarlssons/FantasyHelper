using System.Text;

namespace FH.UI.Blazor.Pages
{
    public partial class Planner
    {
        #region Injections

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public StateContainer StateContainer { get; set; }

        [Inject]
        public IConfigService ConfigService { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        #endregion

        #region Properties

        public static IEnumerable<PlannerPlayerViewModel> Players { get; set; }
        public static string SelectedFantasyGame { get; set; }
        public int GameweeksToDisplay { get; set; } = 5;

        public List<PlannerTeamViewModel> CurrentTeam { get; set; }

        #endregion

        #region Init / Render

        protected override void OnInitialized()
        {
            StateContainer.OnChange += StateHasChanged;

            CurrentTeam = new();
            for (int i = StateContainer.NextGameweek; i <= (StateContainer.NextGameweek + GameweeksToDisplay); i++)
            {
                CurrentTeam.Add(new() { Gameweek = i, Players = new() });
            }
        }

        public void Dispose()
        {
            StateContainer.OnChange -= StateHasChanged;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(StateContainer.SelectedFantasyGame != SelectedFantasyGame)
            {
                try
                {
                    SelectedFantasyGame = StateContainer.SelectedFantasyGame;
                    StateContainer.DataIsLoading = true;
                    ResetData();

                    // Load data
                    if (StateContainer.NextGameweek == 0)
                    {
                        await LoadGameweeks();
                    }

                    Console.WriteLine(StateContainer.NextGameweek);
                    await LoadPlayers();

                    StateHasChanged();
                }
                finally
                {
                    StateContainer.DataIsLoading = false;
                }
            }
        }

        #endregion

        #region Data Loads

        private async Task LoadGameweeks()
        {
            var response = await HttpClient.GetAsync(ConfigService.GetAllGameweeksURL());
            Console.WriteLine(response.StatusCode);

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

                StateContainer.NextGameweek = gameweeks?.FirstOrDefault(gw => gw.IsNext)?.GameweekId ?? 0;
            }
        }

        private async Task LoadPlayers()
        {
            var response = await HttpClient.GetAsync(ConfigService.GetPlannerPlayers());

            if (response.IsSuccessStatusCode)
            {
                var players = JsonSerializer.Deserialize<IEnumerable<PlannerPlayer>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (players == null)
                {
                    Console.WriteLine("Could not parse players");
                    throw new NullReferenceException(nameof(PlannerPlayer));
                }

                Players = Mapper.Map<IEnumerable<PlannerPlayerViewModel>>(players);
            }
        }

        #endregion

        #region Actions

        private void ResetData()
        {
            Players = new List<PlannerPlayerViewModel>();
        }

        public void OnPlayerSelected((int, PlannerTeamCurrentTeamViewModel) payload)
        {
            if (CurrentTeam == null) CurrentTeam = new();

            // Extract payload
            (var gameweek, var player) = payload;

            // Verify that we have teams
            for (int i = gameweek; i <= (gameweek + ((StateContainer.NextGameweek  + GameweeksToDisplay) - gameweek)); i++)
            {
                if (!CurrentTeam.Any(ct => ct.Gameweek == i))
                {
                    CurrentTeam.Add(new() { Gameweek = i, Players = new() { player } });
                }
            }

            // We only want to add players in upcoming fixtures
            foreach (var team in CurrentTeam.Where(t => t.Gameweek >= gameweek))
            {
                // Check if the player exists in the team
                if (team.Players.Any(p => p.Player.PlayerId == player.Player.PlayerId))
                {
                    // Get the index of the player
                    var playerIndex = team.Players.FirstOrDefault(p => p.Player.PlayerId == player.Player.PlayerId).Index;

                    // Check if any player exists on the given index
                    if (team.Players.Any(p => p.Index == player.Index))
                    {
                        // Store that player
                        var movingPlayer = team.Players.FirstOrDefault(p => p.Index == player.Index);

                        // Remove players
                        team.Players.RemoveAll(p => p.Index == player.Index);

                        // Assign new index to moving player
                        movingPlayer.Index = playerIndex;

                        // Add moving player back
                        team.Players.Add(movingPlayer);
                    }

                    // Remove the player
                    team.Players.RemoveAll(p => p.Player.PlayerId == player.Player.PlayerId);
                }
                else
                {
                    // Else we remove the player sitting on the given index
                    team.Players.RemoveAll(p => p.Index == player.Index);
                }

                // Add the player
                team.Players.Add(player);
            }
        }

        #endregion
    }
}
