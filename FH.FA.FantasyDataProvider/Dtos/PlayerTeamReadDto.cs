namespace FantasyHelper.FA.DataProvider.Dtos
{
    /// <summary>
    /// Team displayed in a <see cref="PlayerReadDto"/>
    /// </summary>
    public class PlayerTeamReadDto
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
