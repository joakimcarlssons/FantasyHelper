namespace FantasyHelper.FA.DataProvider.Data
{
    public interface IDataLoader
    {
        Task<ExternalRootDto> LoadRootData();
        Task<bool> SaveToDatabase();
    }
}
