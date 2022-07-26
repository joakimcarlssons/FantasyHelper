namespace FH.FPL.FantasyDataProvider.Dtos
{
    /// <summary>
    /// Fixture displayed in a <see cref="TeamReadDto"/>
    /// </summary>
    public class TeamFixtureReadDto
    {
        public int GameweekId { get; set; }

        public FixtureTeamReadDto Opponent => TeamId == HomeTeam.TeamId ? AwayTeam : HomeTeam;

        public bool IsFinished { get; set; }

        [JsonIgnore]
        public int TeamId { get; set; }

        [JsonIgnore]
        public FixtureTeamReadDto HomeTeam { get; set; }

        [JsonIgnore]
        public FixtureTeamReadDto AwayTeam { get; set; }

    }
}
