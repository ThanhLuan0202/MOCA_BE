using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.MomProfileDTO;
using MOCA_Services.Interfaces;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MomProfileController : ControllerBase
    {
        private readonly IMomProfileService _service;
        private readonly IMapper _mapper;



        public MomProfileController(IMomProfileService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        // GET: api/<MomProfileController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MomProfile>>> GetAllMomProfile()
        {
            var query = await _service.GetAlLPackageAsync();
            var result = _mapper.Map<List<ViewMomProfileModel>>(query);
            return Ok(result);
        }

        // GET api/<MomProfileController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MomProfile>> GetMomProfileGetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mom = await _service.GetMomProfileByIdAsync(id);

            return Ok(mom);

        }

        // POST api/<MomProfileController>
        [HttpPost]
        public async Task<ActionResult<MomProfile>> CreateMomProfile([FromBody] CreateMomProfileModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId =  User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            var newMom = await _service.CreateMomProfileAsync(value, userId);

            return Ok(newMom);

        }

        // PUT api/<MomProfileController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<MomProfile>> UpdateMomProfile(int id, [FromBody] UpdateMomProfileModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mom = await _service.UpdateMomProfileAsync(id, value);

            return Ok(mom);

        }

    }
}
