using Microsoft.AspNetCore.Mvc;
using MOCA_Repositories.Enitities;
using MOCA_Services.Interfaces;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorContactController : ControllerBase
    {
        private readonly IDoctorContactService _doctorContactService;

        public DoctorContactController(IDoctorContactService doctorContactService)
        {
            _doctorContactService = doctorContactService;
        }

        

        // GET api/<DoctorContactController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorContact>> CompleteDoctorContact(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var contact = await _doctorContactService.CompleteDoctorContact(id);

            return Ok(contact);
        }

        // POST api/<DoctorContactController>
        [HttpPost]
        public async Task<ActionResult<DoctorContact>> CreateDoctorContact([FromBody] DoctorContact doctorContact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("User ID not found in claims.");
            }
            var newDoctorContact = await _doctorContactService.CreateDoctorContact(userId, doctorContact);

            return Ok(newDoctorContact);
        }

        

        
    }
}
