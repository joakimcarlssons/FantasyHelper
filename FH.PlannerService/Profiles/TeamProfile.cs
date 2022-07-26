namespace FH.PlannerService.Profiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            // Source -> Target
            CreateMap<TeamPublishedDto, Team>()
                .ForMember(dest => dest.HomeFixtures, opt => opt.Ignore())
                .ForMember(dest => dest.AwayFixtures, opt => opt.Ignore())
                .ForMember(dest => dest.Players, opt => opt.Ignore());
            CreateMap<Team, TeamPlannerReadDto>();
        }
    }
}
