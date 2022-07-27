namespace FH.PlannerService.Profiles
{
    public class FixtureProfile : Profile
    {
        public FixtureProfile()
        {
            // Source -> Target
            CreateMap<FixturePublishedDto, Fixture>()
                .ForMember(dest => dest.HomeTeamId, opt => opt.MapFrom(src => src.HomeTeam.TeamId))
                .ForMember(dest => dest.HomeTeamDifficulty, opt => opt.MapFrom(src => src.HomeTeam.Difficulty))
                .ForMember(dest => dest.AwayTeamId, opt => opt.MapFrom(src => src.AwayTeam.TeamId))
                .ForMember(dest => dest.AwayTeamDifficulty, opt => opt.MapFrom(src => src.AwayTeam.Difficulty))
                .ForMember(dest => dest.HomeTeam, opt => opt.Ignore())
                .ForMember(dest => dest.AwayTeam, opt => opt.Ignore());
            CreateMap<Fixture, FixturePlannerReadDto>()
                .AfterMap((src, dest) => dest.HomeTeam.Difficulty = src.AwayTeamDifficulty)
                .AfterMap((src, dest) => dest.AwayTeam.Difficulty = src.HomeTeamDifficulty);

        }
    }
}
