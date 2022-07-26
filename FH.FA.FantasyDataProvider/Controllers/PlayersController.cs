using Microsoft.AspNetCore.Mvc;

namespace FH.FA.FantasyDataProvider.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        #region Private Members

        private readonly IDataProviderRepository _repo;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public PlayersController(IDataProviderRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        #endregion

        #region Endpoints

        [HttpGet]
        public ActionResult<IEnumerable<PlayerReadDto>> GetAllPlayers()
        {
            var players = _repo.GetAllPlayers();
            return Ok(_mapper.Map<IEnumerable<PlayerReadDto>>(players));
        }

        [HttpGet("{id}")]
        public ActionResult<PlayerReadDto> GetPlayerById(int id)
        {
            var player = _repo.GetPlayerById(id);
            return Ok(_mapper.Map<PlayerReadDto>(player));
        }

        #endregion
    }
}
