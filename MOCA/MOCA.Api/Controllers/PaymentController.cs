using Microsoft.AspNetCore.Mvc;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Services.Interfaces;

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        private readonly MOCAContext _context;

        public PaymentController(IVnPayService vnPayService, MOCAContext context)
        {
            _vnPayService = vnPayService;
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment([FromBody] int courseId)
        {
            var userId = 1; 
            var purchased = new PurchasedCourse
            {
                CourseId = courseId,
                UserId = userId,
                //PurchaseDate = DateTime.UtcNow,
                Status = "Pending"
            };

            _context.PurchasedCourses.Add(purchased);
            await _context.SaveChangesAsync();

            var paymentUrl = _vnPayService.CreatePaymentUrl(purchased, HttpContext);
            return Redirect(paymentUrl);
        }

        [HttpGet("vnpay-return")]
        public async Task<IActionResult> VnPayReturn()
        {
            var vnp_ResponseCode = Request.Query["vnp_ResponseCode"];
            var vnp_TxnRef = Request.Query["vnp_TxnRef"];

            if (vnp_ResponseCode == "00") 
            {
                int purchasedId = int.Parse(vnp_TxnRef);
                var purchase = await _context.PurchasedCourses.FindAsync(purchasedId);
                if (purchase != null)
                {
                    purchase.Status = "Paid";
                    await _context.SaveChangesAsync();
                }
                return Ok(new { message = "Thanh toán thành công" });
            }
            return BadRequest(new { message = "Thanh toán thất bại hoặc bị hủy" });
        }
    }
}
