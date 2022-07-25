namespace FH.UI.Blazor.Components.Fixtures
{
    public partial class FixtureDisplay
    {
        #region Parameters

        [Parameter]
        public List<FixtureViewModel>? Fixtures { get; set; }

        /// <summary>
        /// The team whos opponent will be shown
        /// </summary>
        [Parameter]
        public int TeamId { get; set; }

        [Parameter]
        public int GameweekNumber { get; set; } = 0;

        [Parameter]
        public int NextGameweek { get; set; }

        [Parameter]
        public EventCallback<int> OnGameweekClicked { get; set; }

        #endregion

        private bool IsBlank(FixtureViewModel fixture) => fixture == null ? true : fixture?.HomeTeam?.TeamId != TeamId && fixture?.AwayTeam.TeamId != TeamId;
        private bool IsHome(FixtureViewModel fixture) => fixture?.HomeTeam?.TeamId == TeamId;

        private string SetFixtureBackground(FixtureViewModel fixture)
        {
            if (fixture == null) return "var(--color-fixture-gray)";
            return IsHome(fixture) ? FixtureHelpers.GetFixtureDifficultyColor(fixture.HomeTeam.Difficulty) : FixtureHelpers.GetFixtureDifficultyColor(fixture.AwayTeam.Difficulty);
        }

        private string SetFixtureForeground(FixtureViewModel fixture)
        {
            var fixtureDifficulty = IsHome(fixture) ? fixture.HomeTeam.Difficulty : fixture.AwayTeam.Difficulty;
            if (fixtureDifficulty > 4) return "var(--color-fixture-gray)";
            else return "var(--color-black)";
        }

        private async Task GameweekClicked()
        {
            await OnGameweekClicked.InvokeAsync(GameweekNumber);
        }
    }
}
