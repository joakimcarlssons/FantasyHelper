using System.ComponentModel.DataAnnotations.Schema;

namespace FH.FPL.FantasyDataProvider.Models
{
    public class Fixture
    {
        [Key]
        public int FixtureId { get; set; }
        public int GameweekId { get; set; }
        public int HomeTeamId { get; set; }
        public int HomeTeamDifficulty { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamId { get; set; }
        public int AwayTeamDifficulty { get; set; }
        public int AwayTeamScore { get; set; }
        public bool IsFinished { get; set; }


        public Gameweek Gameweek { get; set; }
        public Team AwayTeam { get; set; }
        public Team HomeTeam { get; set; }
    }
}
