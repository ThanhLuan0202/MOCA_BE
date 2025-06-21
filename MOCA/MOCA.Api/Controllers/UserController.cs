using Microsoft.AspNetCore.Mvc;
using MOCA_Repositories.Enitities;
using MOCA_Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var checkUser = await _userService.GetUserById(id);


            return Ok(checkUser);


        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUser()
        {
            
            var listUser = await _userService.GetAllUser();
            if (listUser == null || !listUser.Any())
            {
                return NotFound("There is no user in the database!");
            }
            return Ok(listUser);
        }

    }
}
