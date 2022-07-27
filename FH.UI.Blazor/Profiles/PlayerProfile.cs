namespace FH.UI.Blazor.Profiles
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            // Source -> Target
            CreateMap<Player, PlayerViewModel>();
            CreateMap<PlannerPlayer, PlannerPlayerViewModel>();
            CreateMap<PlannerPlayerTeam, PlannerPlayerTeamViewModel>();
            CreateMap<PlannerPlayerFixture, PlannerPlayerFixtureViewModel>();
        }
    }
}
