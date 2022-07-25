namespace FH.FA.FixturesProvider.Profiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            // Source -> Target
            CreateMap<Team, FixtureTeamReadDto>();
        }
    }
}
