namespace FH.FA.FixturesProvider.Data
{
    public interface IRepository
    {
        bool SaveChanges();

        // Teams
        void SaveTeam(Team team);
        IEnumerable<Team> GetAllTeams();
        Team GetTeam(Func<Team, bool> filter);

        // Fixtures
        void SaveFixture(Fixture fixture);
        IEnumerable<Fixture> GetAllFixtures();
        Fixture GetFixtureById(int id);
        IEnumerable<Fixture> GetFixturesForGameweek(int gameweekId);
        IEnumerable<Fixture> GetFixturesForTeam(int teamId);
    }
}
