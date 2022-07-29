using Microsoft.AspNetCore.Components.Web;

namespace FH.UI.Blazor.Components.Customs
{
    public partial class PlannerPlayerSearchBar : IDisposable
    {
        #region Parameters

        [Parameter]
        public int Gameweek { get; set; }

        [Parameter]
        public List<PlannerPlayerViewModel>? Players { get; set; }

        [Parameter]
        public int BarIndex { get; set; }

        [Parameter]
        public EventCallback<(int, PlannerPlayerInTeamViewModel)> OnPlayerSelected { get; set; }


        private List<PlannerTeamViewModel> teams;
        [CascadingParameter]
        public List<PlannerTeamViewModel> Teams 
        {
            get => teams;
            set
            {
                teams = value;
                if (teams?.Any(t => t.Gameweek == Gameweek && t.Players.Any(p => p.Index == BarIndex)) ?? false)
                {
                    SetSelectedPlayer(teams.FirstOrDefault(t => t.Gameweek == Gameweek).Players.FirstOrDefault(p => p.Index == BarIndex).Player);
                }
                else
                {
                    ResetSelectedPlayer();
                }
            }
        }

        #endregion

        #region Properties

        private string SearchInput { get; set; }
        private List<PlannerPlayerViewModel>? FilteredPlayers { get; set; } = new();
        private PlannerPlayerViewModel? SelectedPlayer { get; set; }
        private List<FixtureViewModel>? SelectedPlayerFixtures { get; set; } = new();
        public int CurrentHoveredPlayerIndex { get; set; } = -1;
        
        #endregion

        protected override void OnInitialized()
        {
            OnSearch += (input) =>
            {
                SearchInput = input;
                if (String.IsNullOrEmpty(input))
                {
                    SelectedPlayer = new();
                    SetSelectedPlayerFixtures();
                    FilteredPlayers = new();
                }
                else
                {
                    FilteredPlayers = Players?
                        .Where(p => p.FullName.ToLowerInvariant().Contains(input.ToLowerInvariant()) 
                                 || p.DisplayName.ToLowerInvariant().Contains(input.ToLowerInvariant()))
                        .ToList();
                }
            };
        }

        public void Dispose()
        {
            foreach (var action in OnSearch?.GetInvocationList())
            {
                OnSearch -= (Action<string>)action;
            }
        }

        private event Action<string>? OnSearch;
        private void InputFieldChanged(string input) => OnSearch?.Invoke(input);

        private async Task SelectPlayer(PlannerPlayerViewModel player)
        {
            if (player == null) return;

            var selectedPlayer = new PlannerPlayerInTeamViewModel() { Index = BarIndex, Player = player };
            //selectedPlayer.Index = BarIndex;
            await OnPlayerSelected.InvokeAsync((Gameweek, selectedPlayer));

            SetSelectedPlayer(player);

            // Reset search field
            FilteredPlayers = new();
        }

        private void SetSelectedPlayer(PlannerPlayerViewModel player)
        {
            SelectedPlayer = player;
            SetSelectedPlayerFixtures();
            SearchInput = player.DisplayName;
        }

        private void ResetSelectedPlayer()
        {
            SelectedPlayer = new();
            SelectedPlayerFixtures = new();
            SearchInput = "";
        }

        private void SetSelectedPlayerFixtures()
        {
            SelectedPlayerFixtures = new();
            if (SelectedPlayer != null && SelectedPlayer?.Fixtures != null)
            {
                foreach (var fixture in SelectedPlayer?.Fixtures?.Where(f => f.GameweekId == Gameweek))
                {
                    SelectedPlayerFixtures.Add(new()
                    {
                        GameweekId = fixture.GameweekId,
                        HomeTeam = new()
                        {
                            TeamId = fixture.HomeTeam.TeamId,
                            ShortName = fixture.HomeTeam.ShortName,
                            Difficulty = fixture.AwayTeam.Difficulty
                        },
                        AwayTeam = new()
                        {
                            TeamId = fixture.AwayTeam.TeamId,
                            ShortName = fixture.AwayTeam.ShortName,
                            Difficulty = fixture.HomeTeam.Difficulty
                        }
                    });
                }
            }
        }

        private void OnPlayerListHover(MouseEventArgs args, int hoveredPlayerIndex) => CurrentHoveredPlayerIndex = hoveredPlayerIndex;
        private async Task ScrollPlayerList(KeyboardEventArgs args)
        {
            if (args.Key == "ArrowDown")
            {
                CurrentHoveredPlayerIndex += 1;
            }
            else if (args.Key == "ArrowUp")
            {
                CurrentHoveredPlayerIndex -= 1;
            }
            else if (args.Key == "Enter")
            {
                if (FilteredPlayers == null || FilteredPlayers?.Count == 0) return;
                await SelectPlayer(FilteredPlayers?[CurrentHoveredPlayerIndex]);
            }
            else
            {
                CurrentHoveredPlayerIndex = 0;
            }
        }

        private void ResetData()
        {
            SearchInput = "";
            FilteredPlayers = new();
            SelectedPlayerFixtures = new();
            CurrentHoveredPlayerIndex = -1;
        }
    }
}
