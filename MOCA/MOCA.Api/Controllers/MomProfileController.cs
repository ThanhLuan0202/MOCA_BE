using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Manager,Admin")]

        public async Task<ActionResult<IEnumerable<MomProfile>>> GetAllMomProfile()
        {
            var query = await _service.GetAlLPackageAsync();
            return Ok(query);
        }

        // GET api/<MomProfileController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Mom,Doctor,Manager")]

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
        [Authorize(Roles = "User,Manager")]

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
        [Authorize(Roles = "Mom,Manager")]

        public async Task<ActionResult<MomProfile>> UpdateMomProfile(int id, [FromBody] UpdateMomProfileModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mom = await _service.UpdateMomProfileAsync(id, value);

            return Ok(mom);

        }

        [HttpGet("GetUserById")]

        public async Task<ActionResult<MomProfile>> GetMomByUser()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            if (!int.TryParse(userId, out int idd))
            {
                throw new ArgumentException("Invalid user ID");

            }
            var mom = await _service.GetMomProfileByUserIdAsync(idd);

            return Ok(mom);

        }

       

    }
}
