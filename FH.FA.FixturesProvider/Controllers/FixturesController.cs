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

        #endregion
    }
}
