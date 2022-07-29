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

        public List<PlannerPlanViewModel> Plans { get; set; }
        public PlannerPlanViewModel ActivePlan => Plans?.FirstOrDefault(plan => plan.IsActive);
        public List<PlannerTeamViewModel> Teams => ActivePlan?.Teams;

        #endregion

        #region Init / Render

        protected override void OnInitialized()
        {
            StateContainer.OnChange += StateHasChanged;

            Plans = new() { new() { PlanId = 1, IsActive = true, Teams = new() } };
            for (int i = StateContainer.NextGameweek; i <= (StateContainer.NextGameweek + GameweeksToDisplay); i++)
            {
                Plans.FirstOrDefault(p => p.IsActive).Teams.Add(new() { Gameweek = i, Players = new() });
            }
        }

        public void Dispose()
        {
            StateContainer.OnChange -= StateHasChanged;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (StateContainer.SelectedFantasyGame != SelectedFantasyGame)
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

        public void OnPlayerSelected((int, PlannerPlayerInTeamViewModel) payload)
        {
            // Extract payload
            (var gameweek, var selectedPlayer) = payload;

            // Make sure plans are initialized
            if (Plans == null) Plans = new() { new() { IsActive = true }};

            // Get selected plan
            var selectedPlan = Plans.FirstOrDefault(plan => plan.IsActive);

            // Verify that we have teams
            for (int i = gameweek; i <= (gameweek + ((StateContainer.NextGameweek  + GameweeksToDisplay) - gameweek)); i++)
            {
                if (!selectedPlan.Teams.Any(team => team.Gameweek == i))
                {
                    selectedPlan.Teams.Add(new()
                    {
                        Gameweek = i,
                        Players = new() { selectedPlayer }
                    });
                }
            }

            // We only want to add players in upcoming fixtures in the active plan
            foreach (var team in selectedPlan.Teams.Where(t => t.Gameweek >= gameweek))
            {
                // Check if the player exists in the team
                if (team.Players.Any(p => p.PlayerId == selectedPlayer.PlayerId))
                {
                    // Get the index of the player
                    var playerIndex = team.Players.FirstOrDefault(p => p.PlayerId == selectedPlayer.PlayerId).Index;

                    // Check if any player exists on the given index
                    if (team.Players.Any(p => p.Index == selectedPlayer.Index))
                    {
                        // Store that player
                        var movingPlayer = team.Players.FirstOrDefault(p => p.Index == selectedPlayer.Index);

                        // Remove players
                        team.Players.RemoveAll(p => p.Index == selectedPlayer.Index);

                        // Assign new index to moving player
                        movingPlayer.Index = playerIndex;

                        // Add moving player back
                        team.Players.Add(movingPlayer);
                    }

                    // Remove the player
                    team.Players.RemoveAll(p => p.PlayerId == selectedPlayer.PlayerId);
                }
                else
                {
                    // Else we remove the player sitting on the given index
                    team.Players.RemoveAll(p => p.Index == selectedPlayer.Index);
                }

                // Add the player
                team.Players.Add(selectedPlayer);
            }
        }

        public void AddNewPlan()
        {
            Plans.Add(new()
            {
                PlanId = Plans.Count + 1,
                Teams = new(),
                IsActive = false
            });
        }

        public void ActivatePlan(PlannerPlanViewModel plan)
        {
            ActivePlan.IsActive = false;
            Plans.FirstOrDefault(p => p == plan).IsActive = true;
        }

        public void RemovePlan(PlannerPlanViewModel plan)
        {
            // If the active plan is being removed, set previous plan as active
            if (ActivePlan == plan) Plans.FirstOrDefault(p => p.PlanId == plan.PlanId - 1).IsActive = true;

            // Remove plan
            Plans.Remove(plan);
        }

        public void UpdatePlanName(PlannerPlanViewModel plan)
        {
            Plans.FirstOrDefault(p => p.PlanId == plan.PlanId).Name = plan.Name;
        }

        #endregion
    }
}
