using Microsoft.AspNetCore.Mvc;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Services.Interfaces;
using System.Threading.Tasks;

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IBookingPaymentService _bookingPaymentService;

        public PaymentController(IBookingPaymentService bookingPaymentService)
        {
            _bookingPaymentService = bookingPaymentService;
        }

        [HttpGet("paypal-return")]
        public async Task<IActionResult> PayPalReturn([FromQuery] string token)
        {
            //var confirm = await _bookingPaymentService.ConfirmPayment(int.Parse(token));


            return Ok("Thanh toán thành công");
        }

        [HttpGet("paypal-cancel")]
        public IActionResult PayPalCancel()
        {
            return Ok("Thanh toán đã bị huỷ");
        }
    }
}
