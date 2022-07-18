namespace FH.FA.FixturesProvider.Data
{
    public class FixturesRepository : IRepository
    {
        #region Private Members

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private EndpointOptions _endpoints;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public FixturesRepository(AppDbContext context, IMapper mapper, IOptions<EndpointOptions> endpoints)
        {
            _context = context;
            _mapper = mapper;
            _endpoints = endpoints.Value;
        }

        #endregion

        public bool SaveChanges() => (_context.SaveChanges() >= 0);

        public IEnumerable<Fixture> GetAllFixtures()
        {
            return _context.Fixtures
                .Include(f => f.HomeTeam)
                .Include(f => f.AwayTeam)
                .ToList();
        }

        public Fixture GetFixtureById(int id)
        {
            return _context.Fixtures.FirstOrDefault(fixture => fixture.FixtureId == id);
        }

        public IEnumerable<Fixture> GetFixturesForGameweek(int gameweekId)
        {
            return _context.Fixtures.Where(fixture => fixture.GameweekId == gameweekId).ToList();
        }

        public IEnumerable<Fixture> GetFixturesForTeam(int teamId)
        {
            return _context.Fixtures.Where(fixture => fixture.HomeTeamId == teamId || fixture.AwayTeamId == teamId).ToList();
        }

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

        public Team GetTeam(Func<Team, bool> filter)
        {
            return _context.Teams.FirstOrDefault(filter);
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return _context.Teams.ToList();
        }
    }
}
