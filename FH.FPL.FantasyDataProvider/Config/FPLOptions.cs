namespace FH.FPL.FantasyDataProvider.Config
{
    public class FPLOptions
    {
        public string RootEndpoint { get; set; }
        public string FixturesEndpoint { get; set; }
        public string DetailedPlayerDataEndpoint { get; set; }
        public int LoadingInterval { get; set; }
    }
}
