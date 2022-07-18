namespace FantasyHelper.FPL.DataProvider.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; } 
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int TeamCode { get; set; }

        public ICollection<Player> Players { get; set; } = new List<Player>();
        public ICollection<Fixture> HomeFixtures { get; set; } = new List<Fixture>();
        public ICollection<Fixture> AwayFixtures { get; set; } = new List<Fixture>();
    }
}
