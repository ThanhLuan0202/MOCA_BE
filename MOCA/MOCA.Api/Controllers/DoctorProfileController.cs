using Microsoft.AspNetCore.Mvc;
using MOCA_Repositories.Enitities;
using MOCA_Services.Interfaces;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorProfileController : ControllerBase
    {
        private readonly IDoctorProfileService _service;

        public DoctorProfileController(IDoctorProfileService service)
        {
            _service = service;
        }


        // GET: api/<DoctorProfileController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorProfile>>> GetAllDoctorProfile()
        {
           var query = await _service.GetAlLDoctorProfileAsync();

            return Ok(query);
        }

        // GET api/<DoctorProfileController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorProfile>> GetDoctorProfileById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var docPro = await _service.GetDoctorProfileByIdAsync(id);

            return Ok(docPro);
        }

        // POST api/<DoctorProfileController>
        [HttpPost]
        public async Task<ActionResult<DoctorProfile>> CreateDoctorProfile([FromBody] DoctorProfile value)
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
            var newDoctor = await _service.CreateDoctorProfileAsync(value, userId);

            return Ok(newDoctor);

        }

        // PUT api/<DoctorProfileController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<DoctorProfile>> UpdateDoctorProfile(int id, [FromBody] DoctorProfile value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateDoc = await _service.UpdateDoctorProfileAsync(id, value);

            return Ok(updateDoc);

        }

        // DELETE api/<DoctorProfileController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DoctorProfile>> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var deleteDoc = await _service.DeleteDoctorProfileAsync(id);

            return Ok(deleteDoc);

        }
    }
}
