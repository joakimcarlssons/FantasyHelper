﻿namespace FantasyHelper.FPL.DataProvider.Dtos
{
    /// <summary>
    /// Fixture displayed in a <see cref="TeamReadDto"/>
    /// </summary>
    public class TeamFixtureReadDto
    {
        public int Gameweek { get; set; }

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