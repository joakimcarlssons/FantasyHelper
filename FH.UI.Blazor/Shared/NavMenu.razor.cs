namespace FH.UI.Blazor.Shared
{
    public partial class NavMenu : IDisposable
    {
        [Inject]
        public StateContainer StateContainer { get; set; }

        protected override void OnInitialized()
        {
            StateContainer.OnChange += StateHasChanged;
        }

        public void Dispose()
        {
            StateContainer.OnChange -= StateHasChanged;
        }

        #region Navigation

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        /// <summary>
        /// The nav links.
        /// Key     = Title/name of navlink
        /// Value   = Route
        /// </summary>
        public Dictionary<string, string> NavLinks => new()
        {
            { "Home", "/" },
            { "Planner", "/planner" },
            { "Fixtures", "/fixtures" }
        };

        private void NavigateTo(string path)
        {
            NavigationManager?.NavigateTo(path);
        }

        #endregion

        #region Fantasy Selector

        public List<string> FantasyOptions => new()
        {
            "FPL",
            "Fantasy Allsvenskan"
        }; 

        public void OnFantasyChanged(string value)
        {
            StateContainer.SelectedFantasyGame = value;
        }

        #endregion
    }
}
