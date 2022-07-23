namespace FH.UI.Blazor.Components.Helpers
{
    internal static class FixtureHelpers
    {
        internal static string GetFixtureDifficultyColor(int difficulty)
        {
            switch (difficulty)
            {
                case 2: return "#01FC7A";
                case 4: return "#FF1751";
                case 5: return "#80072D";
                default: return "#E7E7E7";
            }
        }
    }
}
