using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOCA_Services.Interfaces;
using Net.payOS.Types;

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayPackageController : ControllerBase
    {

        private readonly IPayPackageService _payPackageService;
        private readonly IBookingPaymentService _bookingPaymentService;
        public PayPackageController(IPayPackageService payPackageService, IBookingPaymentService bookingPaymentService)
        {
            _payPackageService = payPackageService;
            _bookingPaymentService = bookingPaymentService;
        }



        [HttpPost("create-payment-url/{purchasePackageId}")]
        public async Task<IActionResult> CreatePaymentUrl(int purchasePackageId)
        {
            try
            {
                var result = await _payPackageService.CreatePaymentUrl(purchasePackageId);
                return Ok(new { orderId = purchasePackageId, PaymentUrl = result.checkoutUrl, PaymentId = result.paymentLinkId });
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
                var result = _payPackageService.VerifyWebhook(webhook);
                return Ok(new { success = true, data = result });

            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }


    }
}
