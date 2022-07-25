namespace FH.UI.Blazor.Pages
{
    public partial class Planner
    {
        #region Injections

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public StateContainer StateContainer { get; set; }

        [Inject]
        public IConfigService ConfigService { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        #endregion

        #region Properties

        public IEnumerable<PlayerViewModel> Players { get; set; }
        public string SelectedFantasyGame { get; set; }

        #endregion

        #region Init / Render

        protected override void OnInitialized()
        {
            StateContainer.OnChange += StateHasChanged;
        }

        public void Dispose()
        {
            StateContainer.OnChange -= StateHasChanged;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(StateContainer.SelectedFantasyGame != SelectedFantasyGame)
            {
                try
                {
                    SelectedFantasyGame = StateContainer.SelectedFantasyGame;
                    StateContainer.DataIsLoading = true;
                    ResetData();

                    // Load data
                    await LoadPlayers();

                    StateHasChanged();
                }
                finally
                {
                    StateContainer.DataIsLoading = false;
                }
            }
        }

        #endregion

        #region Data Loads

        private async Task LoadPlayers()
        {
            var response = await HttpClient.GetAsync(ConfigService.GetAllPlayersURL());
            if (response.IsSuccessStatusCode)
            {
                var players = JsonSerializer.Deserialize<IEnumerable<Player>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (players == null)
                {
                    Console.WriteLine("Could not parse players");
                    throw new NullReferenceException(nameof(Player));
                }

                Players = Mapper.Map<IEnumerable<PlayerViewModel>>(players);
            }
        }

        #endregion

        #region Actions

        private void ResetData()
        {
            Players = new List<PlayerViewModel>();
        }

        #endregion
    }
}
