namespace FH.FA.FantasyDataProvider.Models
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{ FirstName } { LastName }";
        public string DisplayName { get; set; }
        public decimal Price { get; set; }
        public string Form { get; set; }
        public int? ChanceOfPlayingThisRound { get; set; }
        public int? ChanceOfPlayingNextRound { get; set; }
        public int Position { get; set; }
        public int TeamId { get; set; }
        public string SelectedByPercent { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int CleanSheets { get; set; }
        public int GoalsConceded { get; set; }
        public int MinutesPlayed { get; set; }
        public int Saves { get; set; }
        public int AttackingBonus { get; set; }
        public int DefendingBonus { get; set; }
        public int WinningGoals { get; set; }
        public int Crosses { get; set; }
        public int KeyPasses { get; set; }
        public int BigChancesCreated { get; set; }
        public int ClearancesBlocksAndInterceptions { get; set; }
        public int Recoveries { get; set; }

        public Team Team { get; set; }
    }
}
