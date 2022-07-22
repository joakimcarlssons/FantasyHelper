using FH.UI.Blazor.Components.ViewModels;

namespace FH.UI.Blazor.Profiles
{
    public class FixtureProfile : Profile
    {
        public FixtureProfile()
        {
            // Source -> Target
            CreateMap<Fixture, FixtureViewModel>();
        }
    }
}
