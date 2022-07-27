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
        public int GameweeksToDisplay { get; set; } = 2;

        public List<PlannerTeamViewModel> CurrentTeam { get; set; }

        #endregion

        #region Init / Render

        protected override void OnInitialized()
        {
            StateContainer.OnChange += StateHasChanged;

            CurrentTeam = new();
            for (int i = 1; i <= GameweeksToDisplay; i++)
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

            (var gameweek, var player) = payload;

            if (!CurrentTeam.Any(ct => ct.Gameweek == gameweek))
            {
                CurrentTeam.Add(new() { Gameweek = gameweek, Players = new() { player } });
            }

            // We only want to add players in upcoming fixtures
            foreach (var team in CurrentTeam.Where(t => t.Gameweek >= gameweek))
            {
                // TODO: Make sure we can move players around

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
