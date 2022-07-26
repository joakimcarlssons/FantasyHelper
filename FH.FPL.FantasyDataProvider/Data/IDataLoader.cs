namespace FH.FPL.FantasyDataProvider.Data
{
    public interface IDataLoader
    {
        Task<ExternalRootDto> LoadRootData();
        Task<IEnumerable<ExternalFixtureDto>> LoadFixtureData();

        Task<bool> SaveToDatabase();
    }
}
