using System.Security.Claims;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using MOCA_Repositories.Models.CourseCartDTO;
using MOCA_Services.Interfaces;

namespace MOCA.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseCartController : ControllerBase
    {
        private readonly ICourseCartService _cartService;

        public CourseCartController(ICourseCartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromQuery] int courseId)
        {
            if (!User.Identity?.IsAuthenticated ?? false)
                return Unauthorized("User is not logged in.");

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User not authenticated.");

            if (!int.TryParse(userIdClaim, out var userId))
                return BadRequest("Invalid user ID.");

            var cart = await _cartService.AddToCart(userId, courseId);

            var result = new CartModel
            {
                UserId = cart.UserId,
                Items = cart.Items.Select(x => new CartItemModel
                {
                    CourseId = x.CourseId,
                    Price = x.Price
                }).ToList()
            };

            return Ok(result);
        }


        [HttpGet]
        public IActionResult GetCart()
        {
            if (!User.Identity?.IsAuthenticated ?? false)
                return Unauthorized("User is not logged in.");

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User not authenticated.");

            if (!int.TryParse(userIdClaim, out var userId))
                return BadRequest("Invalid user ID.");

            var cart = _cartService.GetCart(userId);
            return Ok(cart ?? new CartModel { UserId = userId });
        }

        [HttpPost("apply-discount")]
        public async Task<IActionResult> ApplyDiscount([FromBody] ApplyDiscountModel dto)
        {
            if (!User.Identity?.IsAuthenticated ?? false)
                return Unauthorized("User is not logged in.");

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User not authenticated.");

            if (!int.TryParse(userIdClaim, out var userId))
                return BadRequest("Invalid user ID.");

            var cart = await _cartService.ApplyDiscountAsync(userId, dto.DiscountId);
            return Ok(cart);
        }

        [HttpDelete("remove")]
        public IActionResult RemoveFromCart([FromQuery] int courseId)
        {
            if (!User.Identity?.IsAuthenticated ?? false)
                return Unauthorized("User is not logged in.");

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User not authenticated.");

            if (!int.TryParse(userIdClaim, out var userId))
                return BadRequest("Invalid user ID.");

            var result = _cartService.RemoveFromCart(userId, courseId);
            return Ok(new { Removed = result });
        }

        [HttpDelete("clear")]
        public IActionResult ClearCart()
        {
            if (!User.Identity?.IsAuthenticated ?? false)
                return Unauthorized("User is not logged in.");

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User not authenticated.");

            if (!int.TryParse(userIdClaim, out var userId))
                return BadRequest("Invalid user ID.");

            _cartService.ClearCart(userId);
            return Ok("Cart cleared.");
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromQuery] string paymentMethod)
        {
            if (!User.Identity?.IsAuthenticated ?? false)
                return Unauthorized("User is not logged in.");

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User not authenticated.");

            if (!int.TryParse(userIdClaim, out var userId))
                return BadRequest("Invalid user ID.");

            var orderId = await _cartService.CheckoutAsync(userId, paymentMethod);
            return Ok(new { OrderId = orderId });
        }
    }

}
