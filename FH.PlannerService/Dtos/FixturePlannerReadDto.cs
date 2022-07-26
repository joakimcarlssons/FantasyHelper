namespace FH.PlannerService.Dtos
{
    public class FixturePlannerReadDto
    {
        public int GameweekId { get; set; }

        [JsonIgnore]
        public TeamPlannerReadDto HomeTeam { get; set; }

        [JsonIgnore]
        public TeamPlannerReadDto AwayTeam { get; set; }
        public TeamPlannerReadDto Opponent { get; set; }
        public bool IsFinished { get; set; }
    }
}
