namespace FH.FA.FixturesProvider.Profiles
{
    public class FixtureProfile : Profile
    {
        public FixtureProfile()
        {
            // Source -> Target
            CreateMap<ExternalFixtureDto, Fixture>();
            CreateMap<Fixture, FixtureReadDto>()
                .AfterMap((src, dest) => dest.HomeTeam.Difficulty = src.AwayTeamDifficulty)
                .AfterMap((src, dest) => dest.AwayTeam.Difficulty = src.HomeTeamDifficulty);
        }
    }
}
