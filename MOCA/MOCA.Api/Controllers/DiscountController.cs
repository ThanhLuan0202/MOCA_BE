using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.DiscountDTO;
using MOCA_Repositories.Models.FeedbackDTO;
using MOCA_Services.Interfaces;

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService;
        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var discounts = await _discountService.GetAllAsync();
            var result = discounts.Select(d => new ViewDiscountModel
            {
                DiscountId = d.DiscountId,
                Code = d.Code,
                Description = d.Description,
                //DiscountType = d.DiscountType,
                Value = d.Value,
                MaxUsage = d.MaxUsage,
                StartDate = d.StartDate,
                EndDate = d.EndDate,
                IsActive = d.IsActive,
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var discount = await _discountService.GetByIdAsync(id);
            if (discount == null) return NotFound();

            var result = new ViewDiscountModel
            {
                DiscountId = discount.DiscountId,
                Code = discount.Code,
                Description = discount.Description,
                //DiscountType = discount.DiscountType,
                Value = discount.Value,
                MaxUsage = discount.MaxUsage,
                StartDate = discount.StartDate,
                EndDate = discount.EndDate,
                IsActive = discount.IsActive,
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDiscountModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _discountService.AddAsync(model);

                var result = new ViewDiscountModel
                {
                    DiscountId = created.DiscountId,
                    Code = created.Code,
                    Description = created.Description,
                    //DiscountType = created.DiscountType,
                    Value = created.Value,
                    MaxUsage = created.MaxUsage,
                    StartDate = created.StartDate,
                    EndDate = created.EndDate,
                    IsActive = created.IsActive
                };

                return CreatedAtAction(nameof(GetById), new { id = result.DiscountId }, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", detail = ex.Message });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDiscountModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updated = await _discountService.UpdateAsync(id, model);
                if (updated == null)
                    return NotFound(new { message = $"Discount with ID {id} not found." });

                var result = new ViewDiscountModel
                {
                    DiscountId = updated.DiscountId,
                    Code = updated.Code,
                    Description = updated.Description,
                    //DiscountType = updated.DiscountType,
                    Value = updated.Value,
                    MaxUsage = updated.MaxUsage,
                    StartDate = updated.StartDate,
                    EndDate = updated.EndDate,
                    IsActive = updated.IsActive
                };

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", detail = ex.Message });
            }
        }


        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(int id)
        {
            var deleted = await _discountService.DeleteAsync(id);

            if (deleted == null)
                return NotFound(new { message = $"Discount with ID {id} not found." });

            if (deleted.IsActive == true)
                return BadRequest(new { message = "Discount deactivation failed." });

            return Ok(new
            {
                message = "Discount deactivated successfully.",
                DiscountId = deleted.DiscountId,
                IsActive = deleted.IsActive
            });
        }



    }
}
