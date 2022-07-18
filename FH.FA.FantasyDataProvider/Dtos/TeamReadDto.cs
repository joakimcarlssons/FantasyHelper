namespace FantasyHelper.FA.DataProvider.Dtos
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
        public List<TeamFixtureReadDto> HomeFixtures { get; set; } = new();
        public List<TeamFixtureReadDto> AwayFixtures { get; set; } = new();
    }
}
