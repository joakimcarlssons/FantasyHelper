namespace FH.UI.Blazor.Models
{
    public class PlannerPlayer
    {
        public int PlayerId { get; set; }
        public string FullName { get; set; }
        public string DisplayName { get; set; }
        public decimal Price { get; set; }
        public int Position { get; set; }
        public int? ChanceOfPlayingThisRound { get; set; }
        public int? ChanceOfPlayingNextRound { get; set; }
        public PlannerPlayerTeam Team { get; set; }
        public IEnumerable<PlannerPlayerFixture> Fixtures { get; set; }
    }

    public class PlannerPlayerTeam
    {
        public int TeamId { get; set; }
        public string ShortName { get; set; }
        public int Difficulty { get; set; }
    }

    public class PlannerPlayerFixture
    {
        public int GameweekId { get; set; }
        public PlannerPlayerTeam Opponent { get; set; }
        public bool IsFinished { get; set; }
    }
}
