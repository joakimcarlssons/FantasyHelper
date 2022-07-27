namespace FH.UI.Blazor.Config
{
    public class ConfigService : IConfigService
    {
        #region Private Members

        private readonly IConfiguration _config;
        private readonly StateContainer _state;

        private Func<string> CurrentConfig => () =>
        {
            return _state.SelectedFantasyGame switch
            {
                "FPL" => "FPL",
                "Fantasy Allsvenskan" => "FantasyAllsvenskan",
                _ => "",
            };
        };

        private IConfigurationSection CurrentConfigSection => _config.GetSection(CurrentConfig.Invoke());

        #endregion

        #region Constructor

        public ConfigService(IConfiguration config, StateContainer state)
        {
            _config = config;
            _state = state;
        }

        public string GetAllFixturesURL() => CurrentConfigSection.GetValue<string>(FantasyConfigKeys.Fixtures);

        public string GetAllGameweeksURL() => CurrentConfigSection.GetValue<string>(FantasyConfigKeys.Gameweeks);

        public string GetAllPlayersURL() => CurrentConfigSection.GetValue<string>(FantasyConfigKeys.Players);

        public string GetAllTeamsURL() => CurrentConfigSection.GetValue<string>(FantasyConfigKeys.Teams);

        public string GetFixtureByTeamURL(int teamId) => $"{ GetAllFixturesURL() }/team/{ teamId }";

        public string GetPlannerPlayers() => CurrentConfigSection.GetValue<string>(FantasyConfigKeys.PlannerPlayers);

        #endregion
    }

    public class FantasyConfigKeys
    {
        public const string Gameweeks = "Gameweeks";
        public const string Players = "Players";
        public const string Teams = "Teams";
        public const string Fixtures = "Fixtures";
        public const string PlannerPlayers = "PlannerPlayers";
    }
}
