using Microsoft.AspNetCore.Mvc;
using MOCA_Repositories.Enitities;
using MOCA_Services.Interfaces;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasedPackageController : ControllerBase
    {
        private readonly IPurchasedPackageService _service;

        public PurchasedPackageController(IPurchasedPackageService service)
        {
            _service = service;
        }

        // GET: api/<PurchasedPackageController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchasePackage>>> GetAllPurchasePackage()
        {
           var list  = await _service.GetAllAsync();

            return Ok(list);

        }

        // GET api/<PurchasedPackageController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchasePackage>> GetPurchasePackageById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  
            }

            var pc = await _service.GetByIdAsync(id);

            return Ok(pc);
        }

        // POST api/<PurchasedPackageController>
        [HttpPost]
        public async Task<ActionResult<PurchasePackage>> CreatePurchasPackage([FromBody] PurchasePackage value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var newPc = await _service.AddAsync(userId, value);

            return Ok(newPc);


        }

        

        // DELETE api/<PurchasedPackageController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PurchasePackage>> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var pcDel = await _service.DeleteAsync(id);

            return pcDel;

        }


        [HttpGet("GetEnroll")]
        public async Task<ActionResult<IEnumerable<PurchasePackage>>> GetEnrollByUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var pc = await _service.GetEnrolledPurchasePackageIdsByUserNameAsync(userId);

            return Ok(pc);
        }
    }
}
