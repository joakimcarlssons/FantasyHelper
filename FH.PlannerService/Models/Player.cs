namespace FH.PlannerService.Models
{
    public class Player
    {
        [Key]
        public int InternalPlayerId { get; set; }
        public int PlayerId { get; set; }
        public int FantasyId { get; set; }
    }
}
