namespace FH.FPL.FantasyDataProvider.Dtos.External
{
    /// <summary>
    /// The root DTO returned from FPL Api
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
