using System.ComponentModel.DataAnnotations;

namespace FH.FA.FixturesProvider.Models
{
    public class Fixture
    {
        [Key]
        public int FixtureId { get; set; }
        public int GameweekId { get; set; }
        public int HomeTeamId { get; set; }
        public int HomeTeamDifficulty { get; set; }
        public int? HomeTeamScore { get; set; }
        public int AwayTeamId { get; set; }
        public int AwayTeamDifficulty { get; set; }
        public int? AwayTeamScore { get; set; }
        public bool IsFinished { get; set; }

        public Team AwayTeam { get; set; }
        public Team HomeTeam { get; set; }
    }
}
