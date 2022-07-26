namespace FH.FA.DataProvider.Dtos
{
    /// <summary>
    /// Displays a player in a <see cref="TeamReadDto"/>
    /// </summary>
    public class TeamPlayerReadDto
    {
        public string FullName { get; set; }
        public string DisplayName { get; set; }
        public string Form { get; set; }
        public int Position { get; set; }
    }
}
