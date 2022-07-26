namespace FH.PlannerService.Models
{
    public class Fixture
    {
        [Key]
        public int InternalFixtureId { get; set; }
        public int FantasyId { get; set; }
        public int FixtureId { get; set; }
        public int GameweekId { get; set; }
        public bool IsFinished { get; set; }
        public int HomeTeamId { get; set; }
        public int HomeTeamDifficulty { get; set; }
        public int AwayTeamId { get; set; }
        public int AwayTeamDifficulty { get; set; }

        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
    }
}
