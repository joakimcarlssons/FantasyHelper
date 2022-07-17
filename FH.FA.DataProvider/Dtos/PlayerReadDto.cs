namespace FantasyHelper.FA.DataProvider.Dtos
{
    /// <summary>
    /// Standard display of a Player
    /// </summary>
    public class PlayerReadDto
    {
        public int PlayerId { get; set; }
        public string FullName { get; set; }
        public decimal Price { get; set; }
        public PlayerTeamReadDto Team { get; set; }
        public string Form { get; set; }
        public int Position { get; set; }
        public int? ChanceOfPlayingThisRound { get; set; }
        public int? ChanceOfPlayingNextRound { get; set; }
    }
}
