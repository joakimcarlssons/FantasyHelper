namespace FH.UI.Blazor.Components.ViewModels
{
    public class PlannerPlayerViewModel
    {
        public int PlayerId { get; set; }
        public string FullName { get; set; }
        public string DisplayName { get; set; }
        public decimal Price { get; set; }
        public int Position { get; set; }
        public int? ChanceOfPlayingThisRound { get; set; }
        public int? ChanceOfPlayingNextRound { get; set; }
        public PlannerPlayerTeamViewModel Team { get; set; }
        public IEnumerable<PlannerPlayerFixtureViewModel> Fixtures { get; set; }
    }

    public class PlannerPlayerTeamViewModel
    {
        public int TeamId { get; set; }
        public string ShortName { get; set; }
        public int Difficulty { get; set; }
    }

    public class PlannerPlayerFixtureViewModel
    {
        public int GameweekId { get; set; }
        public PlannerPlayerTeamViewModel HomeTeam { get; set; }
        public PlannerPlayerTeamViewModel AwayTeam { get; set; }
        public bool IsFinished { get; set; }
    }
}
