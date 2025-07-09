using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.PackageDTO;
using MOCA_Services.Interfaces;
using System;
using System.Security.Claims;

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentPackageController : ControllerBase
    {
        private readonly IPayPalPackageService _paypalService;
        private readonly MOCAContext _context;

        public PaymentPackageController(IPayPalPackageService paypalService, MOCAContext context)
        {
            _paypalService = paypalService;
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment([FromBody] PurchaseRequest request)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var package = await _context.Packages.FindAsync(request.PackageId);
            if (package == null) return NotFound("Package not found");

            // Tạo bản ghi purchase trước
            var purchase = new PurchasePackage
            {
                UserId = userId,
                PackageId = request.PackageId,
                PurchaseDate = DateTime.UtcNow,
                Status = "Pending"
            };

            _context.PurchasePackages.Add(purchase);
            await _context.SaveChangesAsync();

            // Gắn purchaseId vào returnUrl
            var returnUrl = $"https://moca-d8fxfqgdb4hxg5ha.southeastasia-01.azurewebsites.net/api/payment/return_paypalPackage?purchaseId={purchase.PurchasePackageId}";

            var paymentUrl = await _paypalService.CreatePaymentUrl(package.Price, returnUrl);

            return Ok(new { paymentUrl });
        }


        [HttpGet("paypal-return")]
        public async Task<IActionResult> PaypalReturn([FromQuery] string token, [FromQuery] string PayerID)
        {
            var success = await _paypalService.CapturePaymentAsync(token);
            if (!success) return BadRequest("Thanh toán thất bại");

            var purchase = await _context.PurchasePackages
                .Where(p => p.Status == "Pending")
                .OrderByDescending(p => p.PurchaseDate)
                .FirstOrDefaultAsync();

            if (purchase != null)
            {
                purchase.Status = "Active"; 
                _context.Entry(purchase).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return Content("Thanh toán thành công");
        }
    }
}
