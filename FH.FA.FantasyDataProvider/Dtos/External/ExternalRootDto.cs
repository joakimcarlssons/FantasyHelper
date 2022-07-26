namespace FH.FA.DataProvider.Dtos.External
{
    /// <summary>
    /// The root DTO coming from FA Api
    /// </summary>
    public class ExternalRootDto
    {
        [JsonPropertyName("events")]
        public List<ExternalGameweekDto> Gameweeks { get; set; }

        [JsonPropertyName("teams")]
        public List<ExternalTeamDto> Teams { get; set; }

        [JsonPropertyName("elements")]
        public List<ExternalPlayerDto> Players { get; set; }
    }
}
