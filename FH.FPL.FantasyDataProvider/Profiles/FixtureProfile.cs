namespace FantasyHelper.FPL.DataProvider.Profiles
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
                .AfterMap((src, dest) => dest.HomeTeam.Difficulty = src.HomeTeamDifficulty)
                .AfterMap((src, dest) => dest.AwayTeam.Difficulty = src.AwayTeamDifficulty);
            CreateMap<Fixture, TeamFixtureReadDto>()
                .AfterMap((src, dest) => dest.HomeTeam.Difficulty = src.AwayTeamDifficulty)
                .AfterMap((src, dest) => dest.AwayTeam.Difficulty = src.HomeTeamDifficulty);
            CreateMap<Fixture, GameweekFixtureReadDto>()
                .AfterMap((src, dest) => dest.HomeTeam.Difficulty = src.HomeTeamDifficulty)
                .AfterMap((src, dest) => dest.AwayTeam.Difficulty = src.AwayTeamDifficulty);
        }
    }
}
