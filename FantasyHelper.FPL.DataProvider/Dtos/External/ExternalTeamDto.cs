namespace FantasyHelper.FPL.DataProvider.Dtos.External
{
    /// <summary>
    /// The team DTO returned from FPL Api
    /// </summary>
    public class ExternalTeamDto
    {
        [JsonPropertyName("id")]
        public int TeamId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("short_name")]
        public string ShortName { get; set; }

        [JsonPropertyName("code")]
        public int TeamCode { get; set; }
    }
}
