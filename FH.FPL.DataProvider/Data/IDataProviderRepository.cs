namespace FantasyHelper.FPL.DataProvider.Data
{
    public interface IDataProviderRepository
    {
        bool SaveChanges();

        // Gameweeks
        void SaveGameweek(Gameweek gameweek);
        IEnumerable<Gameweek> GetAllGameweeks();
        Gameweek GetGameweekById(int id);

        // Players
        void SavePlayer(Player player);
        IEnumerable<Player> GetAllPlayers();
        Player GetPlayerById(int id);

        // Teams
        void SaveTeam(Team team);
        IEnumerable<Team> GetAllTeams();
        Team GetTeamById(int id);

        // Fixtures
        void SaveFixture(Fixture fixture);
        IEnumerable<Fixture> GetAllFixtures();
        Fixture GetFixtureById(int id);
    }
}
