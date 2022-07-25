namespace FH.UI.Blazor.Shared
{
    public partial class NavMenu : IDisposable
    {
        [Inject]
        public StateContainer StateContainer { get; set; }

        [Inject]
        public IJSRuntime JS { get; set; }

        protected override async Task OnInitializedAsync()
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

        public async Task OnFantasyChanged(string value)
        {
            StateContainer.SelectedFantasyGame = value;
            await JS.SaveToLocalStorage(LocalStorageKeys.ChosenFantasy, value);
        }

        #endregion
    }
}
