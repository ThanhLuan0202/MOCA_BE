using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOCA_Services.Interfaces;
using Net.payOS.Types;
using Newtonsoft.Json;
using System.Text.Json;

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayOSController : ControllerBase
    {
        private readonly IPayOSService _payOSService;
        private readonly IBookingPaymentService _bookingPaymentService;
        public PayOSController(IPayOSService payOSService, IBookingPaymentService bookingPaymentService)
        {
            _payOSService = payOSService;
            _bookingPaymentService = bookingPaymentService;
        }



        [HttpPost("create-payment-url/{orderId}")]
        public async Task<IActionResult> CreatePaymentUrl(int orderId)
        {
            try
            {
                var result = await _payOSService.CreatePaymentUrl(orderId);
                return Ok(new { orderId = orderId, PaymentUrl = result.checkoutUrl, PaymentId = result.paymentLinkId });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> WebhookHandler(WebhookType webhook)
        {
            try
            {
                var result = _payOSService.VerifyWebhook(webhook);
                return Ok(new { success = true, data = result });

            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                return BadRequest(new { success = false, message = ex.Message });
            }
        }



    }
}
