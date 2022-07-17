namespace FantasyHelper.FA.DataProvider.Models
{
    public class Gameweek
    {
        [Key]
        public int GameweekId { get; set; }
        public bool IsFinished { get; set; }
        public bool IsCurrent { get; set; }
        public DateTime Deadline { get; set; }

        public ICollection<Fixture> Fixtures { get; set; } = new List<Fixture>();
    }
}
