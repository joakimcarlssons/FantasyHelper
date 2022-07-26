namespace FH.FA.DataProvider.Models
{
    public class Gameweek
    {
        [Key]
        public int GameweekId { get; set; }
        public bool IsFinished { get; set; }
        public bool IsCurrent { get; set; }
        public bool IsNext { get; set; }
        public DateTime Deadline { get; set; }
    }
}
