namespace FH.PlannerService.Data
{
    public class PlannerDataRepository : IRepository
    {
        #region Private Members

        private readonly AppDbContext _context;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public PlannerDataRepository(AppDbContext context)
        {
            _context = context;
        }

        #endregion

        public bool SaveChanges() => (_context.SaveChanges() >= 0);

        public IEnumerable<Fixture> GetAllFixtures()
        {
            return _context.Fixtures.ToList();
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return _context.Players.ToList();
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return _context.Teams.ToList();
        }

        public void SaveFixture(Fixture fixture)
        {
            if (fixture == null) throw new NullReferenceException(nameof(Fixture));
            if (_context.Fixtures.Any(f => f.FixtureId == fixture.FixtureId && f.FantasyId == fixture.FantasyId))
            {
                // Get internal id
                fixture.InternalFixtureId = _context.Fixtures.FirstOrDefault(f => f.FixtureId == fixture.FixtureId && f.FantasyId == fixture.FantasyId).InternalFixtureId;
                _context.Fixtures.Update(fixture);
            }
            else
            {
                _context.Fixtures.Add(fixture);
            }
        }

        public void SavePlayer(Player player)
        {
            if (player == null) throw new NullReferenceException(nameof(Player));
            if (_context.Players.Any(p => p.PlayerId == player.PlayerId && p.FantasyId == player.FantasyId))
            {
                // Get internal id
                player.InternalPlayerId = _context.Players.FirstOrDefault(p => p.PlayerId == player.PlayerId && p.FantasyId == player.FantasyId).InternalPlayerId;
                _context.Players.Update(player);
            }
            else
            {
                _context.Players.Add(player);
            }
        }

        public void SaveTeam(Team team)
        {
            if (team == null) throw new NullReferenceException(nameof(Team));
            if (_context.Teams.Any(t => t.TeamId == team.TeamId && t.FantasyId == team.FantasyId))
            {
                // Get internal id
                team.InternalTeamId = _context.Teams.FirstOrDefault(t => t.TeamId == team.TeamId && t.FantasyId == team.FantasyId).InternalTeamId;
                _context.Teams.Update(team);
            }
            else
            {
                _context.Teams.Add(team);
            }
        }

        public IEnumerable<Team> GetAllTeamsByFantasyId(int fantasyId)
        {
            return _context.Teams.Where(team => team.FantasyId == fantasyId).ToList();
        }

        public IEnumerable<Player> GetAllPlayersByFantasyId(int fantasyId)
        {
            return _context.Players.Where(player => player.FantasyId == fantasyId).ToList();
        }

        public IEnumerable<Fixture> GetAllFixturesByFantasyId(int fantasyId)
        {
            return _context.Fixtures.Where(fixture => fixture.FantasyId == fantasyId).ToList();
        }
    }
}
