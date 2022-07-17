namespace FantasyHelper.FA.DataProvider.Profiles
{
    public class TeamProfile : Profile
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public TeamProfile()
        {
            // Source -> Target
            CreateMap<ExternalTeamDto, Team>();
            CreateMap<Team, TeamReadDto>()
                .AfterMap((src, dest) => dest.HomeFixtures.ForEach(f => f.TeamId = src.TeamId))
                .AfterMap((src, dest) => dest.AwayFixtures.ForEach(f => f.TeamId = src.TeamId));

            // Used when returning a player
            CreateMap<Team, PlayerTeamReadDto>();

            // Used when returning a fixture (for the opponent)
            CreateMap<Team, FixtureTeamReadDto>();
        }
    }
}
