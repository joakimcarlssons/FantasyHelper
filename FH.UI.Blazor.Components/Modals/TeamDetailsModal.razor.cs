namespace FH.UI.Blazor.Components.Modals
{
    public partial class TeamDetailsModal
    {
        #region Parameters

        [Parameter]
        public TeamViewModel? Team { get; set; } = new();

        [Parameter]
        public List<FixtureViewModel>? TeamFixtures { get; set; } = new();

        [Parameter]
        public int NextGameweek { get; set; }

        #endregion
    }
}
