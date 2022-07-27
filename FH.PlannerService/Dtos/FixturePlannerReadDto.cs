namespace FH.PlannerService.Dtos
{
    public class FixturePlannerReadDto
    {
        public int GameweekId { get; set; }
        public TeamPlannerReadDto HomeTeam { get; set; }
        public TeamPlannerReadDto AwayTeam { get; set; }
        public bool IsFinished { get; set; }
    }
}
