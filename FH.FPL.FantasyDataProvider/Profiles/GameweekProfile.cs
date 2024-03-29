﻿namespace FH.FPL.FantasyDataProvider.Profiles
{
    public class GameweekProfile : Profile
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public GameweekProfile()
        {
            // Source -> Target
            CreateMap<ExternalGameweekDto, Gameweek>();
            CreateMap<Gameweek, GameweekReadDto>();
        }
    }
}
