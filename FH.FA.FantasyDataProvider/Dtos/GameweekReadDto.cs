﻿namespace FH.FA.FantasyDataProvider.Dtos
{
    /// <summary>
    /// Standard display of a gameweek
    /// </summary>
    public class GameweekReadDto
    {
        public int GameweekId { get; set; }
        public bool IsFinished { get; set; }
        public bool IsCurrent { get; set; }

        public bool IsNext { get; set; }
        public DateTime Deadline { get; set; }
    }
}
