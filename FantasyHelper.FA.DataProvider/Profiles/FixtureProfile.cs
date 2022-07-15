namespace FantasyHelper.FA.DataProvider.Profiles
{
    public class FixtureProfile : Profile
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public FixtureProfile()
        {
            // Source -> Target
            CreateMap<ExternalFixtureDto, Fixture>();
            CreateMap<Fixture, FixtureReadDto>()
                .ForMember(dest => dest.Gameweek, opt => opt.MapFrom(source => source.GameweekId))
                .AfterMap((src, dest) => dest.HomeTeam.Difficulty = src.AwayTeamDifficulty)
                .AfterMap((src, dest) => dest.AwayTeam.Difficulty = src.HomeTeamDifficulty);
            CreateMap<Fixture, TeamFixtureReadDto>()
                .ForMember(dest => dest.Gameweek, opt => opt.MapFrom(source => source.GameweekId))
                .AfterMap((src, dest) => dest.HomeTeam.Difficulty = src.AwayTeamDifficulty)
                .AfterMap((src, dest) => dest.AwayTeam.Difficulty = src.HomeTeamDifficulty);
            CreateMap<Fixture, GameweekFixtureReadDto>()
                .AfterMap((src, dest) => dest.HomeTeam.Difficulty = src.HomeTeamDifficulty)
                .AfterMap((src, dest) => dest.AwayTeam.Difficulty = src.AwayTeamDifficulty);
        }
    }
}
