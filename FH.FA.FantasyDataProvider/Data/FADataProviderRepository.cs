namespace FH.FA.DataProvider.Data
{
    public class FADataProviderRepository : IDataProviderRepository
    {
        #region Pivate Members

        private readonly AppDbContext _context;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public FADataProviderRepository(AppDbContext context)
        {
            _context = context;
        }

        #endregion

        public IEnumerable<Gameweek> GetAllGameweeks()
        {
            return _context.Gameweeks
                .ToList();
        }

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
                .ToList();
        }

        public Gameweek GetGameweekById(int id)
        {
            return _context.Gameweeks
                .FirstOrDefault(gameweek => gameweek.GameweekId == id);
        }

        public Player GetPlayerById(int id)
        {
            return _context.Players.Include(p => p.Team).FirstOrDefault(p => p.PlayerId == id);
        }

        public Team GetTeamById(int id)
        {
            return _context.Teams
                .Include(t => t.Players)
                .FirstOrDefault(t => t.TeamId == id);
        }

        public bool SaveChanges() => (_context.SaveChanges() >= 0);

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
