using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.DiscountDTO;
using MOCA_Repositories.Models.PurchasedCourseDTO;
using MOCA_Services.Interfaces;

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasedCourseController : ControllerBase
    {
        private readonly IPurchasedCourseServices _purchasedCourseServices;
        public PurchasedCourseController(IPurchasedCourseServices purchasedCourseServices)
        {
            _purchasedCourseServices = purchasedCourseServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var purchasedCourses = await _purchasedCourseServices.GetAllAsync();
            var result = purchasedCourses.Select(p => new ViewPurchasedCourseModel
            {
                PurchasedId = p.PurchasedId,
                CourseId = p.CourseId,
                UserId = p.UserId,
                PurchaseDate = p.PurchaseDate,
                Status = p.Status,
                DiscountId = p.DiscountId,
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var purchasedCourse = await _purchasedCourseServices.GetByIdAsync(id);
            if (purchasedCourse == null) return NotFound();

            var result = new ViewPurchasedCourseModel
            {
                PurchasedId = purchasedCourse.PurchasedId,
                CourseId = purchasedCourse.CourseId,
                UserId = purchasedCourse.UserId,
                PurchaseDate = purchasedCourse.PurchaseDate,
                Status = purchasedCourse.Status,
                DiscountId = purchasedCourse.DiscountId,
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePurchasedCourseModel model)
        {
            if (!User.Identity?.IsAuthenticated ?? false)
                return Unauthorized("User is not authenticated.");

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized("Invalid user identifier.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _purchasedCourseServices.AddAsync(userId, model);

                var result = new ViewPurchasedCourseModel
                {
                    PurchasedId = created.PurchasedId,
                    CourseId = created.CourseId,
                    UserId = created.UserId,
                    PurchaseDate = created.PurchaseDate,
                    Status = created.Status,
                    DiscountId = created.DiscountId,
                };

                return CreatedAtAction(nameof(GetById), new { id = result.PurchasedId }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An internal error occurred.", detail = ex.Message });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePurchasedCourseModel model)
        {
            if (!User.Identity?.IsAuthenticated ?? false)
                return Unauthorized("User is not authenticated.");

            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdStr, out var userIdInt))
                return Unauthorized("Invalid user ID.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var purchased = await _purchasedCourseServices.GetByIdAsync(id); 
                if (purchased == null)
                    return NotFound(new { message = $"Purchased course with ID {id} not found." });

                if (purchased.UserId != userIdInt)
                    return Forbid("You are not allowed to update this course purchase.");

                var updated = await _purchasedCourseServices.UpdateAsync(id, model);

                var result = new ViewPurchasedCourseModel
                {
                    PurchasedId = updated.PurchasedId,
                    CourseId = updated.CourseId,
                    UserId = updated.UserId,
                    PurchaseDate = updated.PurchaseDate,
                    Status = updated.Status,
                    DiscountId = updated.DiscountId,
                };

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An internal error occurred.", detail = ex.Message });
            }
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.Identity?.IsAuthenticated ?? false)
                return Unauthorized("User is not authenticated.");

            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdStr, out var userIdInt))
                return Unauthorized("Invalid user ID.");

            var purchased = await _purchasedCourseServices.GetByIdAsync(id);
            if (purchased == null)
                return NotFound(new { message = $"PurchasedCourse with ID {id} not found." });

            if (purchased.UserId != userIdInt)
                return Forbid("You are not allowed to delete this course purchase.");

            var deleted = await _purchasedCourseServices.DeleteAsync(id);

            if (deleted == null)
                return NotFound(new { message = $"PurchasedCourse with ID {id} not found or already inactive." });

            return Ok(new
            {
                message = "PurchasedCourse deactivated successfully.",
                DiscountId = deleted.DiscountId,
                Status = deleted.Status
            });
        }


    }
}

