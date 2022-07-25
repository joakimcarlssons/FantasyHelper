namespace FH.UI.Blazor.Components.Helpers
{
    internal static class FixtureHelpers
    {
        internal static string GetFixtureDifficultyColor(int difficulty)
        {
            switch (difficulty)
            {
                case 2: return "var(--color-fixture-green)";
                case 4: return "var(--color-fixture-red)";
                case 5: return "var(--color-fixture-darkred)";
                default: return "var(--color-fixture-gray)";
            }
        }

        internal static string GetFixtureResultColor(int teamId, FixtureViewModel fixture)
        {
            var teamIsHome = fixture?.HomeTeam?.TeamId == teamId;

            if (fixture?.HomeTeamScore == fixture?.AwayTeamScore) return "var(--color-fixture-gray)";
            else if (fixture?.HomeTeamScore > fixture?.AwayTeamScore) return teamIsHome ? "var(--color-fixture-green)" : "var(--color-fixture-darkred)";
            else return teamIsHome ? "var(--color-fixture-darkred)" : "var(--color-fixture-green)";
        }
    }
}
