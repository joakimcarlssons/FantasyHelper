namespace FH.PlannerService.Dtos
{
    public class TeamPlannerReadDto
    {
        public int TeamId { get; set; }
        public string ShortName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Difficulty { get; set; }
    }
}
