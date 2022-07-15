namespace FantasyHelper.FPL.DataProvider.Data
{
    public interface IDataLoader
    {
        Task<ExternalRootDto> LoadRootData();
        Task<IEnumerable<ExternalFixtureDto>> LoadFixtureData();

        Task<bool> SaveToDatabase();
    }
}
