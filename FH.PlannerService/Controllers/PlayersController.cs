using Microsoft.AspNetCore.Mvc;

namespace FH.PlannerService.Controllers
{
    [ApiController]
    [Route("api/planner/[controller]")]
    public class PlayersController : Controller
    {
        #region Private Members

        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public PlayersController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        #endregion

        #region Endpoints

        [HttpGet]
        public ActionResult<IEnumerable<PlayerPlannerReadDto>> GetAllPlayers()
        {
            try
            {
                var players = _repo.GetAllPlayers();
                return Ok(_mapper.Map<IEnumerable<PlayerPlannerReadDto>>(players));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("{fantasyId}")]
        public ActionResult<IEnumerable<PlayerPlannerReadDto>> GetAllPlayersByFantasyId(int fantasyId)
        {
            try
            {
                var players = _repo.GetAllPlayersByFantasyId(fantasyId);
                return Ok(_mapper.Map<IEnumerable<PlayerPlannerReadDto>>(players));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
