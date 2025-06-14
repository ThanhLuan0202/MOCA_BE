using Microsoft.AspNetCore.Mvc;
using MOCA_Repositories.Enitities;
using MOCA_Services.Interfaces;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PregnancyDiaryController : ControllerBase
    {

        private readonly IPregnancyDiaryService _service;

        public PregnancyDiaryController(IPregnancyDiaryService service)
        {
            _service = service;
        }
        // GET: api/<PregnancyDiaryController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PregnancyDiary>>> GetAllPregnancyDiary()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var query = await _service.GetAlLPregnancyDiary(userId);

            return Ok(query);

        }

        // GET api/<PregnancyDiaryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PregnancyDiary>> GetPregnancyDiaryById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var pregnancyDiary = await _service.GetPregnancyDiaryById(id);

            return Ok(pregnancyDiary);
        }

        // POST api/<PregnancyDiaryController>
        [HttpPost]
        public async Task<ActionResult<PregnancyDiary>> CreatePregnancyDiary([FromBody] PregnancyDiary value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            var newPreg = await _service.CreatePregnancyDiary(value, userId);
            
            return Ok(newPreg);
        }

        // PUT api/<PregnancyDiaryController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<PregnancyDiary>> UpdatePregnancyDiary(int id, [FromBody] PregnancyDiary value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedPregnancyDiary = await _service.UpdateMomProfileAsync(id, value);
            return Ok(updatedPregnancyDiary);
        }

        // DELETE api/<PregnancyDiaryController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PregnancyDiary>> DeletePregnancyDiary(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deletePreg = await _service.DeletePregnancyDiaryById(id);

            return Ok(deletePreg);
        }
    }
}
