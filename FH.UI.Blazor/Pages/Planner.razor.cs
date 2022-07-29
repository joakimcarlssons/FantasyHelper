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

        [Inject]
        public IJSRuntime JS { get; set; }

        #endregion

        #region Properties

        public static IEnumerable<PlannerPlayerViewModel> Players { get; set; }
        public static string SelectedFantasyGame { get; set; }
        public int GameweeksToDisplay { get; set; } = 5;

        public List<PlannerPlanViewModel> Plans { get; set; }
        public PlannerPlanViewModel ActivePlan => Plans?.FirstOrDefault(plan => plan.IsActive) ?? Plans?.FirstOrDefault();
        public List<PlannerTeamViewModel> Teams => ActivePlan?.Teams;

        #endregion

        #region Init / Render

        protected override async Task OnInitializedAsync()
        {
            StateContainer.OnChange += StateHasChanged;

            ResetPlans();
            var plans = await JS.LoadFromLocalStorage<IEnumerable<PlannerPlanViewModel>>($"{ StateContainer.SelectedFantasyGame }_Plans");
            if (plans != null)
            {
                Plans = plans.ToList();
            }


            for (int i = StateContainer.NextGameweek; i <= (StateContainer.NextGameweek + GameweeksToDisplay); i++)
            {
                Plans?.FirstOrDefault(p => p.IsActive)?.Teams?.Add(new() { Gameweek = i, Players = new() });
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

                    if (!firstRender)
                    {
                        var plans = await JS.LoadFromLocalStorage<IEnumerable<PlannerPlanViewModel>>($"{ StateContainer.SelectedFantasyGame }_Plans");
                        if (plans != null)
                        {
                            Plans = plans.ToList();
                        }
                    }

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
            ResetPlans();
        }

        private void ResetPlans()
        {
            Plans = new() { new() { PlanId = 1, IsActive = true, Teams = new() } };
        }

        public void OnPlayerSelected((int, PlannerPlayerInTeamViewModel) payload)
        {
            // Extract payload
            (var gameweek, var selectedPlayer) = payload;

            Console.WriteLine($"Incoming gameweek: " + gameweek);

            // Make sure plans are initialized
            if (Plans == null || Plans.Count <= 0) Plans = new() { new() { IsActive = true, Teams = new() }};

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
                if (team.Players.Any(p => p.Player.PlayerId == selectedPlayer.Player.PlayerId))
                {
                    // Get the index of the player
                    var playerIndex = team.Players.FirstOrDefault(p => p.Player.PlayerId == selectedPlayer.Player.PlayerId).Index;

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
                    team.Players.RemoveAll(p => p.Player.PlayerId == selectedPlayer.Player.PlayerId);
                }
                else
                {
                    // Else we remove the player sitting on the given index
                    team.Players.RemoveAll(p => p.Index == selectedPlayer.Index);
                }

                // Add the player
                team.Players.Add(selectedPlayer);
            }

            // Save changes
            Task.Run(async () => await JS.SaveToLocalStorage($"{ StateContainer.SelectedFantasyGame }_Plans", Plans));
        }

        public void AddNewPlan()
        {
            Plans.Add(new()
            {
                PlanId = Plans.Count + 1,
                Teams = new(),
                IsActive = false
            });

            // Save changes
            Task.Run(async () => await JS.SaveToLocalStorage($"{ StateContainer.SelectedFantasyGame }_Plans", Plans) );
        }

        public void ActivatePlan(PlannerPlanViewModel plan)
        {
            if (ActivePlan != null) ActivePlan.IsActive = false;

            var planToActivate = Plans.FirstOrDefault(p => p.PlanId == plan.PlanId);
            if (planToActivate != null) planToActivate.IsActive = true;

            // Save changes
            Task.Run(async () => await JS.SaveToLocalStorage($"{ StateContainer.SelectedFantasyGame }_Plans", Plans));
        }

        public void RemovePlan(PlannerPlanViewModel plan)
        {
            // If the active plan is being removed, set previous plan as active
            if (ActivePlan?.PlanId == plan.PlanId)
            {
                var index = Plans.IndexOf(ActivePlan);
                Plans[index - 1].IsActive = true;
            }
               
            // Remove plan
            Plans.RemoveAll(p => p.PlanId == plan.PlanId);

            // Save changes
            Task.Run(async () => await JS.SaveToLocalStorage($"{ StateContainer.SelectedFantasyGame }_Plans", Plans));
        }

        public void UpdatePlanName(PlannerPlanViewModel plan)
        {
            Plans.FirstOrDefault(p => p.PlanId == plan.PlanId).Name = plan.Name;

            // Save changes
            Task.Run(async () => await JS.SaveToLocalStorage($"{ StateContainer.SelectedFantasyGame }_Plans", Plans));
        }

        #endregion
    }
}
