namespace FH.FA.FantasyDataProvider.Profiles
{
    public class PlayerProfile : Profile
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public PlayerProfile()
        {
            // Source -> Target
            CreateMap<ExternalPlayerDto, Player>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(source => Convert.ToDecimal(source.Price) / 10));
            CreateMap<Player, PlayerReadDto>();
            CreateMap<Player, TeamPlayerReadDto>();
        }
    }
}
