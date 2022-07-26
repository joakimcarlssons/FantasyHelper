namespace FH.FA.DataProvider.Dtos.External
{
    /// <summary>
    /// The Team DTO coming from FA Api
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
