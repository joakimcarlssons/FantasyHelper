namespace FH.FPL.FantasyDataProvider.Data
{
    public interface IDataLoader
    {
        Task<ExternalRootDto> LoadRootData();
        Task<IEnumerable<ExternalFixtureDto>> LoadFixtureData();

        Task<bool> SaveToDatabase();

        void PublishTeams(IDataProviderRepository repo);
        void PublishPlayers(IDataProviderRepository repo);
        void PublishFixtures(IDataProviderRepository repo);
    }
}
