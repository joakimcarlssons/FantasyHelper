namespace FantasyHelper.FPL.DataProvider.Data
{
    public class FPLDataProviderRepository : IDataProviderRepository
    {
        #region Private Members

        private readonly AppDbContext _context;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public FPLDataProviderRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Fixture> GetAllFixtures()
        {
            return _context.Fixtures
                .Include(f => f.HomeTeam)
                .Include(f => f.AwayTeam)
                .ToList();
        }

        public IEnumerable<Gameweek> GetAllGameweeks()
        {
            return _context.Gameweeks
                .Include(gw => gw.Fixtures)
                    .ThenInclude(f => f.AwayTeam)
                .Include(gw => gw.Fixtures)
                    .ThenInclude(f => f.HomeTeam)
                .ToList();
        }

        #endregion

        public IEnumerable<Player> GetAllPlayers()
        {
            return _context.Players
                .Include(p => p.Team)
                .ToList();
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return _context.Teams
                .Include(t => t.Players)
                .Include(t => t.HomeFixtures)
                    .ThenInclude(f => f.AwayTeam)
                .Include(t => t.AwayFixtures)
                    .ThenInclude(f => f.HomeTeam)
                .ToList();
        }

        public Fixture GetFixtureById(int id)
        {
            return _context.Fixtures
                .Include(f => f.HomeTeam)
                .Include(_f => _f.AwayTeam)
                .FirstOrDefault(fixture => fixture.FixtureId == id);
        }

        public IEnumerable<Fixture> GetFixturesForGameweek(int gameweekId)
        {
            return _context.Fixtures
                .Where(fixture => fixture.GameweekId == gameweekId)
                .Include(f => f.HomeTeam)
                .Include(f => f.AwayTeam)
                .ToList();
        }

        public IEnumerable<Fixture> GetFixturesForTeam(int teamId)
        {
            return _context.Fixtures
                .Where(fixture => fixture.HomeTeamId == teamId || fixture.AwayTeamId == teamId)
                .Include(f => f.HomeTeam)
                .Include(f => f.AwayTeam)
                .ToList();
        }

        public Gameweek GetGameweekById(int id)
        {
            return _context.Gameweeks
                .Include(gw => gw.Fixtures)
                    .ThenInclude(f => f.AwayTeam)
                .Include(gw => gw.Fixtures)
                    .ThenInclude(f => f.HomeTeam)
                .FirstOrDefault(gameweek => gameweek.GameweekId == id);
        }

        public Player GetPlayerById(int id)
        {
            return _context.Players
                .Include(p => p.Team)
                .FirstOrDefault(p => p.PlayerId == id);
        }

        public Team GetTeamById(int id)
        {
            return _context.Teams
                .Include(t => t.Players)
                .Include(t => t.HomeFixtures)
                    .ThenInclude(f => f.AwayTeam)
                .Include(t => t.AwayFixtures)
                    .ThenInclude(f => f.HomeTeam)
                .FirstOrDefault(t => t.TeamId == id);
        }

        public bool SaveChanges() => (_context.SaveChanges() >= 0);

        public void SaveFixture(Fixture fixture)
        {
            if (fixture == null) throw new NullReferenceException(nameof(fixture));
            if (_context.Fixtures.Any(f => f.FixtureId == fixture.FixtureId))
            {
                _context.Fixtures.Update(fixture);
            }
            else
            {
                _context.Fixtures.Add(fixture);
            }
        }

        public void SaveGameweek(Gameweek gameweek)
        {
            if (gameweek == null) throw new NullReferenceException(nameof(gameweek));
            if (_context.Gameweeks.Any(gw => gw.GameweekId == gameweek.GameweekId))
            {
                _context.Gameweeks.Update(gameweek);
            }
            else
            {
                _context.Gameweeks.Add(gameweek);
            }
        }

        public void SavePlayer(Player player)
        {
            if (player == null) throw new NullReferenceException(nameof(player));
            if (_context.Players.Any(p => p.PlayerId == player.PlayerId))
            {
                _context.Players.Update(player);
            }
            else
            {
                _context.Players.Add(player);
            }
        }

        public void SaveTeam(Team team)
        {
            if (team == null) throw new NullReferenceException(nameof(team));
            if (_context.Teams.Any(t => t.TeamId == team.TeamId))
            {
                _context.Teams.Update(team);
            }
            else
            {
                _context.Teams.Add(team);
            }
        }
    }
}
