namespace FH.FA.FantasyDataProvider.Dtos
{
    /// <summary>
    /// Standard display of a team
    /// </summary>
    public class TeamReadDto
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public List<TeamPlayerReadDto> Players { get; set; } = new();
    }
}
