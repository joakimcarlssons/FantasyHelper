namespace FH.PlannerService.Models
{
    public class Team
    {
        [Key]
        public int InternalTeamId { get; set; }
        public int TeamId { get; set; }
        public int FantasyId { get; set; }
    }
}
