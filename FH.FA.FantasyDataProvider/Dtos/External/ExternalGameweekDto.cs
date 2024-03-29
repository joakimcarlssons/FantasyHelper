﻿namespace FH.FA.FantasyDataProvider.Dtos.External
{
    /// <summary>
    /// The Gameweek DTO coming from FA Api
    /// </summary>
    public class ExternalGameweekDto
    {
        [JsonPropertyName("id")]
        public int Gameweekid { get; set; }

        [JsonPropertyName("finished")]
        public bool IsFinished { get; set; }

        [JsonPropertyName("is_current")]
        public bool IsCurrent { get; set; }

        [JsonPropertyName("is_next")]
        public bool IsNext { get; set; }

        [JsonPropertyName("deadline_time")]
        public DateTime Deadline { get; set; }
    }
}
