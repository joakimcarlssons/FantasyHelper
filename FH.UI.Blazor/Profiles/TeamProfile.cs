namespace FH.UI.Blazor.Profiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            // Source -> Target
            CreateMap<Team, TeamViewModel>();
            CreateMap<FixtureTeam, FixtureTeamViewModel>();
        }
    }
}
