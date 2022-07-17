using Microsoft.AspNetCore.Mvc;

namespace FantasyHelper.FA.DataProvider.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : Controller
    {
        #region Private Members

        private readonly IDataProviderRepository _repo;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TeamsController(IDataProviderRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        #endregion

        #region Endpoints

        [HttpGet]
        public ActionResult<IEnumerable<TeamReadDto>> GetAllTeams()
        {
            var teams = _repo.GetAllTeams();
            return Ok(_mapper.Map<IEnumerable<TeamReadDto>>(teams));
        }

        [HttpGet("{id}")]
        public ActionResult<TeamReadDto> GetTeamById(int id)
        {
            var team = _repo.GetTeamById(id);
            var t = _mapper.Map<TeamReadDto>(team);
            return Ok(_mapper.Map<TeamReadDto>(team));
        }

        #endregion
    }
}
