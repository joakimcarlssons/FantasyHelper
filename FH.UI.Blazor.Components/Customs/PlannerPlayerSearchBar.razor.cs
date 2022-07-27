using Microsoft.AspNetCore.Components.Web;

namespace FH.UI.Blazor.Components.Customs
{
    public partial class PlannerPlayerSearchBar : IDisposable
    {
        #region Parameters

        [Parameter]
        public int Gameweek { get; set; }

        private List<PlannerPlayerViewModel>? players;

        [Parameter]
        public List<PlannerPlayerViewModel>? Players
        {
            get => players;
            set
            {
                if (players == value) return;
                else
                {
                    players = value;
                    ResetData();
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

        private void SelectPlayer(PlannerPlayerViewModel player)
        {
            if (player == null) return;

            SelectedPlayer = player;
            SetSelectedPlayerFixtures();
            SearchInput = player.DisplayName;
            FilteredPlayers = new();
        }

        private void SetSelectedPlayerFixtures()
        {
            SelectedPlayerFixtures = new();
            if (SelectedPlayer != null || SelectedPlayer.Fixtures != null)
            {
                foreach (var fixture in SelectedPlayer.Fixtures.Where(f => f.GameweekId == Gameweek))
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
        private void ScrollPlayerList(KeyboardEventArgs args)
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
                SelectPlayer(FilteredPlayers?[CurrentHoveredPlayerIndex]);
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
