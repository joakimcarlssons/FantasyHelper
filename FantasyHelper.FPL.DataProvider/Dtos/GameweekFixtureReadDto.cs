namespace FantasyHelper.FPL.DataProvider.Dtos
{
    /// <summary>
    /// Displays a fixture in a <see cref="GameweekReadDto"/>
    /// </summary>
    public class GameweekFixtureReadDto
    {
        public FixtureTeamReadDto HomeTeam { get; set; }
        public FixtureTeamReadDto AwayTeam { get; set; }
        public bool IsFinished { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? HomeTeamScore { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? AwayTeamScore { get; set; }
    }
}
