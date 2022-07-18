namespace FH.FA.FixturesProvider.Data
{
    public interface IDataLoader
    {
        Task<bool> LoadFixtureData();
        Task<bool> LoadLeagueData();
        bool UpdateFixtureDifficulties();
    }
}
