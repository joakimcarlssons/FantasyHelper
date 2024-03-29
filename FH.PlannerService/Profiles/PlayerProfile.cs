﻿namespace FH.PlannerService.Profiles
{
    public class PlayerProfile :Profile
    {
        public PlayerProfile()
        {
            // Source -> Target
            CreateMap<PlayerPublishedDto, Player>();
            CreateMap<Player, PlayerPlannerReadDto>()
                .ForMember(dest => dest.Fixtures, opt => opt.MapFrom(src => src.Team.HomeFixtures.Union(src.Team.AwayFixtures).OrderBy(f => f.GameweekId)));
        }
    }
}
