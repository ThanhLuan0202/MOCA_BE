using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.AdvertisementDTO;
using MOCA_Services.Interfaces;

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private readonly IAdvertisementService _advertisementService;
        public AdvertisementController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var advertisements = await _advertisementService.GetAllAsync();
            var result = advertisements.Select(a => new ViewAdvertisementModel
            {
                AdId = a.AdId,
                Title = a.Title,
                ImageUrl = a.ImageUrl,
                RedirectUrl = a.RedirectUrl,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                CreateDate = a.CreateDate,
                IsVisible = a.IsVisible
            }).ToList();

            return Ok(result);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveAds()
        {
            var advertisements = await _advertisementService.GetActiveAdsAsync();
            var result = advertisements.Select(a => new ViewAdvertisementModel
            {
                AdId = a.AdId,
                Title = a.Title,
                ImageUrl = a.ImageUrl,
                RedirectUrl = a.RedirectUrl,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                CreateDate = a.CreateDate,
                IsVisible = a.IsVisible
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var advertisement = await _advertisementService.GetByIdAsync(id);
            if (advertisement == null) return NotFound();

            var result = new ViewAdvertisementModel
            {
                AdId = advertisement.AdId,
                Title = advertisement.Title,
                ImageUrl = advertisement.ImageUrl,
                RedirectUrl = advertisement.RedirectUrl,
                StartDate = advertisement.StartDate,
                EndDate = advertisement.EndDate,
                CreateDate = advertisement.CreateDate,
                IsVisible = advertisement.IsVisible
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAdvertisementModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _advertisementService.AddAsync(model);

                var result = new ViewAdvertisementModel
                {
                    AdId = created.AdId,
                    Title = created.Title,
                    ImageUrl = created.ImageUrl,
                    RedirectUrl = created.RedirectUrl,
                    StartDate = created.StartDate,
                    EndDate = created.EndDate,
                    CreateDate = created.CreateDate,
                    IsVisible = created.IsVisible
                };

                return CreatedAtAction(nameof(GetById), new { id = result.AdId }, result);
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
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAdvertisementModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updated = await _advertisementService.UpdateAsync(id, model);
                if (updated == null)
                    return NotFound(new { message = $"Advertisement with ID {id} not found." });

                var result = new ViewAdvertisementModel
                {
                    AdId = updated.AdId,
                    Title = updated.Title,
                    ImageUrl = updated.ImageUrl,
                    RedirectUrl = updated.RedirectUrl,
                    StartDate = updated.StartDate,
                    EndDate = updated.EndDate,
                    CreateDate = updated.CreateDate,
                    IsVisible = updated.IsVisible
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


        [HttpPut("{id}/visibility")]
        public async Task<IActionResult> SetVisibility(int id, [FromQuery] bool visible)
        {
            var ad = await _advertisementService.SetVisibilityAsync(id, visible);

            if (ad == null)
                return NotFound(new { message = $"Advertisement with ID {id} not found." });

            if (ad.IsVisible != visible)
            {
                return BadRequest(new
                {
                    message = visible
                        ? "Failed to activate the advertisement."
                        : "Failed to deactivate the advertisement."
                });
            }

            return Ok(new
            {
                message = visible
                    ? "Advertisement activated successfully."
                    : "Advertisement deactivated successfully.",
                AdId = ad.AdId,
                IsVisible = ad.IsVisible
            });
        }

    }
}
