using System.ComponentModel.DataAnnotations;

namespace FH.FA.FixturesProvider.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public int MatchesPlayed { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsConceded { get; set; }
        public int GoalDifference { get; set; }
        public int Points { get; set; }

        public ICollection<Fixture> HomeFixtures { get; set; } = new List<Fixture>();
        public ICollection<Fixture> AwayFixtures { get; set; } = new List<Fixture>();
    }
}
