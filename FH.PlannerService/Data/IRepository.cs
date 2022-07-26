namespace FH.PlannerService.Data
{
    public interface IRepository
    {
        bool SaveChanges();

        void SaveTeam(Team team);

        void SavePlayer(Player player);
        IEnumerable<Player> GetAllPlayers();
        IEnumerable<Player> GetAllPlayersByFantasyId(int fantasyId);

        void SaveFixture(Fixture fixture);
    }
}
