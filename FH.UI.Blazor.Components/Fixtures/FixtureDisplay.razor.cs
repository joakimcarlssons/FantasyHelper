using FH.UI.Blazor.Components.ViewModels;
using Microsoft.AspNetCore.Components;

namespace FH.UI.Blazor.Components.Fixtures
{
    public partial class FixtureDisplay
    {
        #region Parameters

        [Parameter]
        public List<FixtureViewModel> Fixtures { get; set; } = new();

        /// <summary>
        /// The team whos opponent will be shown
        /// </summary>
        [Parameter]
        public int TeamId { get; set; }

        [Parameter]
        public int GameweekNumber { get; set; } = 0;

        #endregion

        private bool IsBlank(FixtureViewModel fixture) => fixture == null ? true : fixture?.HomeTeam?.TeamId != TeamId && fixture?.AwayTeam.TeamId != TeamId;
        private bool IsHome(FixtureViewModel fixture) => fixture?.HomeTeam?.TeamId == TeamId;

        private string SetFixtureBackground(FixtureViewModel fixture)
        {
            if (fixture == null) return "#E7E7E7";

            var fixtureDifficulty = IsHome(fixture) ? fixture.HomeTeam.Difficulty : fixture.AwayTeam.Difficulty;
            switch (fixtureDifficulty)
            {
                case 2: return "#01FC7A";
                case 4: return "#FF1751";
                case 5: return "#80072D";
                default: return "#E7E7E7";
            }
        }

        private string SetFixtureForeground(FixtureViewModel fixture)
        {
            var fixtureDifficulty = IsHome(fixture) ? fixture.HomeTeam.Difficulty : fixture.AwayTeam.Difficulty;
            if (fixtureDifficulty > 4) return "#E7E7E7";
            else return "#000000";
        }
    }
}
