namespace FH.FPL.FantasyDataProvider.Dtos
{
    /// <summary>
    /// Display of a team in a <see cref="TeamFixtureReadDto"/>
    /// </summary>
    public class FixtureTeamReadDto
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int Difficulty { get; set; }
    }
}
