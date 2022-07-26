namespace FH.FA.FantasyDataProvider.Profiles
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
            CreateMap<Team, TeamReadDto>();

            // Used when returning a player
            CreateMap<Team, PlayerTeamReadDto>();
        }
    }
}
