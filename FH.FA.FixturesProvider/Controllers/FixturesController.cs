using Microsoft.AspNetCore.Mvc;

namespace FH.FA.FixturesProvider.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FixturesController : ControllerBase
    {
        #region Private Members

        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public FixturesController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        #endregion

        #region Endpoints

        [HttpGet]
        public ActionResult<IEnumerable<FixtureReadDto>> GetAllFixtures()
        {
            var fixtures = _repo.GetAllFixtures();
            return Ok(_mapper.Map<IEnumerable<FixtureReadDto>>(fixtures));
        }

        [HttpGet("{fixtureId}")]
        public ActionResult<FixtureReadDto> GetFixtureById(int fixtureId)
        {
            var fixture = _repo.GetFixtureById(fixtureId);
            return Ok(_mapper.Map<FixtureReadDto>(fixture));
        }

        [HttpGet("team/{teamId}")]
        public ActionResult<IEnumerable<FixtureReadDto>> GetFixturesForTeam(int teamId)
        {
            var fixtures = _repo.GetFixturesForTeam(teamId);
            return Ok(_mapper.Map<IEnumerable<FixtureReadDto>>(fixtures));
        }

        [HttpGet("gameweek/{gameweekId}")]
        public ActionResult<IEnumerable<FixtureReadDto>> GeFixturesForGameweek(int gameweekId)
        {
            var fixtures = _repo.GetFixturesForGameweek(gameweekId);
            return Ok(_mapper.Map<IEnumerable<FixtureReadDto>>(fixtures));
        }

        #endregion
    }
}
