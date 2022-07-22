namespace FH.UI.Blazor.Models
{
    public class Fixture
    {
        public int FixtureId { get; set; }
        public int GameweekId { get; set; }
        public FixtureTeam HomeTeam { get; set; }
        public FixtureTeam AwayTeam { get; set; }
        public bool IsFinished { get; set; }
        public int? HomeTeamScore { get; set; }
        public int? AwayTeamScore { get; set; }
    }
}
