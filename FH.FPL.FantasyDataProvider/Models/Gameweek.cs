namespace FH.FPL.FantasyDataProvider.Models
{
    public class Gameweek
    {
        [Key]
        public int GameweekId { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsCurrent { get; set; }
        public bool IsNext { get; set; }
        public bool IsFinished { get; set; }

        public ICollection<Fixture> Fixtures { get; set; } = new List<Fixture>();
    }
}
