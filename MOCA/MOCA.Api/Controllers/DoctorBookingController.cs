using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOCA_Repositories.Enitities;
using MOCA_Services.Interfaces;
using System.Security.Claims;

namespace MOCA.Api.Controllers
{
    [Authorize(Roles = "Mom,User,Doctor,Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorBookingController : ControllerBase
    {
        private readonly IDoctorBookingService _service;

        public DoctorBookingController(IDoctorBookingService service)
        {
            _service = service;
        }

        // GET: api/DoctorBooking/GetAllByUser
        [HttpGet("GetAllByUser")]
        public async Task<ActionResult<IEnumerable<DoctorBooking>>> GetAllDoctorBookingByUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var bookings = await _service.GettAllDoctorBookingByUserId(userId);
            return Ok(bookings);
        }

        // GET: api/DoctorBooking/GetAllByDoctor
        [HttpGet("GetAllByDoctor")]
        public async Task<ActionResult<IEnumerable<DoctorBooking>>> GetAllDoctorBookingByDoctor()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var bookings = await _service.GettAllDoctorBookingByDoctorId(userId);
            return Ok(bookings);
        }

        // GET: api/DoctorBooking/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorBooking>> GetDoctorBookingById(int id)
        {
            var booking = await _service.GettDoctorBookingById(id);
            return Ok(booking);
        }

        // POST: api/DoctorBooking
        [HttpPost]
        public async Task<ActionResult> CreateDoctorBooking([FromBody] DoctorBooking bookingRequest)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var (booking, paymentUrl) = await _service.CreateDoctorBooking(bookingRequest, userId);

            return Ok(new
            {
                message = "Tạo lịch và tạo thanh toán thành công",
                booking,
                paymentUrl
            });
        }

        // PUT: api/DoctorBooking/confirmBooking/5
        [HttpGet("confirmBooking/{id}")]
        public async Task<ActionResult<DoctorBooking>> ConfirmBooking(int id)
        {
            var result = await _service.ConfirmDoctorBooking(id);
            return Ok(result);
        }

        // PUT: api/DoctorBooking/completeBooking/5
        [HttpGet("completeBooking/{id}")]
        public async Task<ActionResult<DoctorBooking>> CompleteBooking(int id)
        {
            var result = await _service.BookingEnd(id);
            return Ok(result);
        }

        // DELETE: api/DoctorBooking/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DoctorBooking>> CancelDoctorBooking(int id)
        {
            var result = await _service.CancelDoctorBooking(id);
            return Ok(result);
        }
    }
}
