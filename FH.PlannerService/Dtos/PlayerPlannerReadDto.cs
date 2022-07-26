namespace FH.PlannerService.Dtos
{
    /// <summary>
    /// Dto used to for displaying players in a planner view
    /// </summary>
    public class PlayerPlannerReadDto
    {
        public int PlayerId { get; set; }
        public string FullName { get; set; }
        public string DisplayName { get; set; }
        public decimal Price { get; set; }
        public int Position { get; set; }
        public int? ChanceOfPlayingThisRound { get; set; }
        public int? ChanceOfPlayingNextRound { get; set; }
        public TeamPlannerReadDto Team { get; set; }
        public IEnumerable<FixturePlannerReadDto> Fixtures { get; set; }
    }
}
