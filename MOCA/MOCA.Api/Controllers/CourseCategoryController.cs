using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MOCA_Repositories.Models.CourseCategoryDTO;
using MOCA_Repositories.Models.FeedbackDTO;
using MOCA_Services.Interfaces;
using MOCA_Services.Services;

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseCategoryController : Controller
    {
        private readonly ICourseCategoryService _courseCategoryService;
        public CourseCategoryController(ICourseCategoryService courseCategoryService)
        {
            _courseCategoryService = courseCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _courseCategoryService.GetAllAsync();
            var result = categories.Select(c => new ViewCourseCategoryModel
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _courseCategoryService.GetByIdAsync(id);
            if (category == null) return NotFound();

            var result = new ViewCourseCategoryModel
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCourseCategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var created = await _courseCategoryService.AddAsync(model);

            var result = new ViewCourseCategoryModel
            {
                CategoryId= created.CategoryId,
                Name = created.Name,
            };

            return CreatedAtAction(nameof(GetById), new { id = result.CategoryId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCourseCategoryModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _courseCategoryService.UpdateAsync(id, model);
            if (updated == null) return NotFound();

            var result = new ViewCourseCategoryModel
            {
                CategoryId = updated.CategoryId,
                Name = updated.Name,
            };

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _courseCategoryService.DeleteAsync(id);
            if (!deleted)
            {
                return BadRequest(new { message = "Delete failed" });
            }

            return Ok(new { message = "Delete successful" });
        }
    }
}
