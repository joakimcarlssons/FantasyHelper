namespace FH.FA.FixturesProvider.Dtos
{
    /// <summary>
    /// Standard display of a fixture
    /// </summary>
    public class FixtureReadDto
    {
        public int GameweekId { get; set; }
        public FixtureTeamReadDto HomeTeam { get; set; }
        public FixtureTeamReadDto AwayTeam { get; set; }
        public bool IsFinished { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? HomeTeamScore { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? AwayTeamScore { get; set; }
    }
}
