using Microsoft.AspNetCore.Mvc;
using MOCA_Repositories.Enitities;
using MOCA_Services.Interfaces;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorBookingController : ControllerBase
    {
        private readonly IDoctorBookingService _service;


        public DoctorBookingController(IDoctorBookingService service)
        {
            _service = service;
        }



        // GET: api/<DoctorBookingController>
        [HttpGet("GetAllByUser")]
        public async Task<ActionResult<IEnumerable<DoctorBooking>>> GetAllDoctorBookingByUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var query = await _service.GettAllDoctorBookingByUserId(userId);

            return Ok(query);



        }

        [HttpGet("GetAllByDoctor")]
        public async Task<ActionResult<IEnumerable<DoctorBooking>>> GetAllDoctorBookingByDoctor()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var query = await _service.GettAllDoctorBookingByDoctorId(userId);

            return Ok(query);



        }

        // GET api/<DoctorBookingController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorBooking>> GetDoctorBookingById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctorBooking = await _service.GettDoctorBookingById(id);

            return Ok(doctorBooking);
        }

        //POST api/<DoctorBookingController>
        [HttpPost]
        public async Task<ActionResult<DoctorBooking>> CreateDoctorBooking([FromBody] DoctorBooking value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var newDtBk = await _service.CreateDoctorBooking(value, userId);

            return Ok(newDtBk);
        }

        

        // DELETE api/<DoctorBookingController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DoctorBooking>> CancelDoctorBooking(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bkCancel = await _service.CancelDoctorBooking(id);

            return Ok(bkCancel);
        }



        [HttpGet("completeBooking/{id}")]
        public async Task<ActionResult<DoctorBooking>> CompleteBooking(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctorBooking = await _service.BookingEnd(id);

            return Ok(doctorBooking);
        }



        [HttpGet("confirmBooking/{id}")]
        public async Task<ActionResult<DoctorBooking>> ConfirmBooking(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctorBooking = await _service.ConfirmDoctorBooking(id);

            return Ok(doctorBooking);
        }
    }
}
