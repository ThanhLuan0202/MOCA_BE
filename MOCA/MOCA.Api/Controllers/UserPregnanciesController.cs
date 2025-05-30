using Microsoft.AspNetCore.Mvc;
using MOCA_Repositories.Enitities;
using MOCA_Services.Interfaces;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPregnanciesController : ControllerBase
    {
        private readonly IUserPregnanciesService _service;

        public UserPregnanciesController(IUserPregnanciesService service)
        {
            _service = service;
        }

        // GET: api/<UserPregnanciesController>
        [HttpGet]
        public async Task<ActionResult< IEnumerable<UserPregnancy>>> GetAllUserPregnancy()
        {
            var query = await _service.GetAlLUserPregnancyAsync();

            return Ok(query);

        }

        // GET api/<UserPregnanciesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserPregnancy>> GetUserPregnancyById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userPre = await _service.GetUserPregnancyByIdAsync(id);

            return Ok(userPre);

        }

        // POST api/<UserPregnanciesController>
        [HttpPost]
        public async Task<ActionResult<UserPregnancy>> CreateUserPregnancy([FromBody] UserPregnancy value)
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

            var newUserPre = await _service.CreateUserPregnancyAsync(value, userId);

            return Ok(newUserPre);

        }

        // PUT api/<UserPregnanciesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserPregnancy>> UpdateUserPregnancy(int id, [FromBody] UserPregnancy value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateUserPre = await _service.UpdateUserPregnancyAsync(id, value);

            return Ok(updateUserPre);
        }

        
    }
}
