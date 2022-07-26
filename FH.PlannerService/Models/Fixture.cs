namespace FH.PlannerService.Models
{
    public class Fixture
    {
        [Key]
        public int InternalFixtureId { get; set; }
        public int FixtureId { get; set; }
        public int FantasyId { get; set; }
    }
}
