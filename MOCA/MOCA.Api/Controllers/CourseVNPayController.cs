using Microsoft.AspNetCore.Mvc;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.CoursePaymentDTO;
using MOCA_Services.Interfaces;
using MOCA_Services.Services;

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseVNPayController : ControllerBase
    {
        private readonly ICourseVnPayService _vnPayService;
        private readonly IOrderCourseService _orderCourseService;
        private readonly IPurchasedCourseServices _purchasedCourseService;
        private readonly ICoursePaymentService _coursePaymentService;
        public CourseVNPayController(ICourseVnPayService vnPayService, IOrderCourseService orderCourseService, IPurchasedCourseServices purchasedCourseService, ICoursePaymentService coursePaymentService)
        {
            _vnPayService = vnPayService;
            _orderCourseService = orderCourseService;
            _purchasedCourseService = purchasedCourseService;
            _coursePaymentService = coursePaymentService;
        }

        [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequest request)
        {
            var order = await _orderCourseService.GetByIdWithDetailsAsync(request.OrderId);

            if (order == null)
                return NotFound("Order not found.");

            if (order.Status != "Pending")
                return BadRequest("This order is already processed or invalid.");

            var paymentUrl = _vnPayService.CreatePaymentUrl(order, HttpContext);
            return Ok(new { url = paymentUrl });
            //return Redirect(paymentUrl);
        }


        [HttpGet("vnpay-return")]
        public async Task<IActionResult> VnPayReturn()
        {
            var vnpQuery = Request.Query;
            var vnp_responsecode = vnpQuery["vnp_ResponseCode"].ToString();
            var vnp_txnref = vnpQuery["vnp_TxnRef"].ToString();

            if (string.IsNullOrEmpty(vnp_txnref))
                return BadRequest(new { message = "Invalid order code." });

            var parts = vnp_txnref.Split('_');
            if (parts.Length < 1 || !int.TryParse(parts[0], out int orderId))
                return BadRequest(new { message = "Invalid order code." });

            var order = await _orderCourseService.GetByIdWithDetailsAsync(orderId);
            if (order == null)
                return NotFound(new { message = "Order not found." });

            string status;
            string message;

            switch (vnp_responsecode)
            {
                case "00":
                    order.Status = "Paid";
                    status = "Success";
                    message = "Payment successful and course recorded.";
                    await _purchasedCourseService.CreateFromOrderAsync(order);
                    break;

                case "24":
                    order.Status = "Cancelled";
                    status = "Cancelled";
                    message = "Payment was cancelled by the user.";
                    break;

                case "07":
                case "10":
                    order.Status = "Suspected";
                    status = "Suspected";
                    message = "Payment suspected as fraud or OTP failed.";
                    break;

                default:
                    order.Status = "Failed";
                    status = "Failed";
                    message = "Payment failed or was declined.";
                    break;
            }

            await _orderCourseService.UpdateAsync(order);
            await _coursePaymentService.CreateFromVnPayAsync(order, vnpQuery, status);

            if (order.Status == "Paid")
                return Ok(new { message });
            return BadRequest(new { message });
        }




    }
}
