namespace FH.PlannerService.Data
{
    public interface IRepository
    {
        bool SaveChanges();

        void SaveTeam(Team team);
        IEnumerable<Team> GetAllTeams();
        IEnumerable<Team> GetAllTeamsByFantasyId(int fantasyId);

        void SavePlayer(Player player);
        IEnumerable<Player> GetAllPlayers();
        IEnumerable<Player> GetAllPlayersByFantasyId(int fantasyId);

        void SaveFixture(Fixture fixture);
        IEnumerable<Fixture> GetAllFixtures();
        IEnumerable<Fixture> GetAllFixturesByFantasyId(int fantasyId);
    }
}
