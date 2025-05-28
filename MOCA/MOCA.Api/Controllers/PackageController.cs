using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.PackageDTO;
using MOCA_Services.Interfaces;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackageService _packageService;
        private readonly IMapper _mapper;

        public PackageController(IPackageService packageService, IMapper mapper)
        {
            _mapper = mapper;
            _packageService = packageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Package>>> GetAllPackage()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var query = await _packageService.GetAlLPackageAsync();
            return Ok(_mapper.Map<List<ViewPackageModel>>(query));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Package>> GetPackageById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var package = await _packageService.GetPackageByIdAsync(id);

            return Ok(package);
        }

        // POST api/<PackageController>
        [HttpPost]
        public async Task<ActionResult<Package>> CreatePackage([FromBody] CreatePackageModel newPackage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var packageNew = await _packageService.CreatePackageAsync(newPackage); 

            return Ok(newPackage);
        }

        // PUT api/<PackageController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Package>> UpdatePackage(int id, [FromBody] UpdatePackageModel updatePackage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var packageUpdate = await _packageService.UpdatePackageAsync(id, updatePackage);

            return Ok(updatePackage);

        }

        // DELETE api/<PackageController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Package>> DeletePackage(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var deletePackage = await _packageService.DeletePackageAsync(id);

            return Ok(deletePackage);


        }
    }
}
