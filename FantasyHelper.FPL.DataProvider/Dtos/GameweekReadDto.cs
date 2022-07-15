namespace FantasyHelper.FPL.DataProvider.Dtos
{
    /// <summary>
    /// Standard display of a gameweek
    /// </summary>
    public class GameweekReadDto
    {
        public int GameweekId { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsCurrent { get; set; }
        public bool IsNext { get; set; }
        public bool IsFinished { get; set; }
        public List<GameweekFixtureReadDto> Fixtures { get; set; }
    }
}
