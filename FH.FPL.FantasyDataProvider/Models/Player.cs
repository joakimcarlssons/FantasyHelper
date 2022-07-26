namespace FH.FPL.FantasyDataProvider.Models
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
        public int Bonus { get; set; }
        public int Bps { get; set; }
        public int? CornersOrder { get; set; }
        public int? FreekicksOrder { get; set; }
        public int? PenaltiesOrder { get; set; }


        public Team Team { get; set; }
    }
}
