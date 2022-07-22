namespace FH.UI.Blazor.Models
{
    public class Gameweek
    {
        public int GameweekId { get; set; }
        public bool IsFinished { get; set; }
        public bool IsCurrent { get; set; }
        public bool IsNext { get; set; }
        public DateTime Deadline { get; set; }
    }
}
