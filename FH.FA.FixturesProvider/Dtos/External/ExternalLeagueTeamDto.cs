namespace FH.FA.FixturesProvider.Dtos.External
{
    /// <summary>
    /// The DTO for a team in a league table fetched from Footystats
    /// </summary>
    public class ExternalLeagueTeamDto
    {
        public string TeamName { get; set; }
        public int Position { get; set; }
        public int MatchesPlayed { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsConceded { get; set; }
        public int GoalDifference { get; set; }
        public int Points { get; set; }
    }
}
