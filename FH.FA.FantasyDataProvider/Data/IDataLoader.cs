namespace FH.FA.FantasyDataProvider.Data
{
    public interface IDataLoader
    {
        Task<ExternalRootDto> LoadRootData();
        Task<bool> SaveToDatabase();
    }
}
