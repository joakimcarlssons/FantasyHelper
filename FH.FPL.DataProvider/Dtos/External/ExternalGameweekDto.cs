namespace FantasyHelper.FPL.DataProvider.Dtos.External
{
    /// <summary>
    /// The Gameweek DTo returned from FPL Api
    /// </summary>
    public class ExternalGameweekDto
    {
        [JsonPropertyName("id")]
        public int GameweekId { get; set; }

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
