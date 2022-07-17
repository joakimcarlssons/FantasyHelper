using Microsoft.AspNetCore.Mvc;

namespace FantasyHelper.FPL.DataProvider.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameweeksController : ControllerBase
    {
        #region Private Members

        private readonly IDataProviderRepository _repo;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public GameweeksController(IDataProviderRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        #endregion

        #region Endpoints

        [HttpGet]
        public ActionResult<IEnumerable<GameweekReadDto>> GetAllGameweeks()
        {
            var gameweeks = _repo.GetAllGameweeks();
            return Ok(_mapper.Map<IEnumerable<GameweekReadDto>>(gameweeks));
        }

        [HttpGet("{id}")]
        public ActionResult<GameweekReadDto> GetGameweekById(int id)
        {
            var gameweek = _repo.GetGameweekById(id);
            return Ok(_mapper.Map<GameweekReadDto>(gameweek));
        }

        #endregion
    }
}
