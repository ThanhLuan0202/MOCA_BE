using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MOCA_Repositories;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.Login;
using MOCA_Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly IAuthenService _authenService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MOCAContext _context;

        public AuthenController(IMapper mapper, IAuthenService authenService, IUnitOfWork unitOfWork, MOCAContext context)
        {
            _authenService = authenService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _context = context;
        }
        
        

        [HttpPost]
        [Route("Login")]

        public async Task<ActionResult<User>> Login([FromBody] LoginModel loginModel)
        {
            if (loginModel == null || string.IsNullOrEmpty(loginModel.Email) || string.IsNullOrEmpty(loginModel.Password))
            {
                return BadRequest(new { code = 400, message = "Invalid username or password" });
            }


            try
            {
                _unitOfWork.BeginTransaction();
                var token = await _authenService.Login(loginModel);
                if (string.IsNullOrEmpty((string?)token))
                {
                    return Unauthorized(new { code = 401, message = "Invalid username or password" });
                }
                // Lưu token vào cookie HTTP-only
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = false,
                    Secure = true,   // Chạy trên HTTPS
                    SameSite = SameSiteMode.None, // Hoặc SameSiteMode.Lax nếu chỉ cần GET requests
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    Path = "/",
                    //Domain = "coursev1.vercel.app"
                };
                Response.Cookies.Append("authToken", (string)token, cookieOptions);
                _unitOfWork.CommitTransaction();

                return Ok(new { code = 200, token = token, message = "Login successful" });
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                return StatusCode(StatusCodes.Status500InternalServerError, new { code = 500, message = ex.Message });
            }
        }
        [HttpPost]
        [Route("Register")]

        public async Task<ActionResult<User>> Register([FromForm] RegisterLoginModel registerLoginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newUser = _mapper.Map<User>(registerLoginModel);

            await _authenService.Register(registerLoginModel);

            return Ok(newUser);
        }



        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail(string email)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
                if (user == null)
                {
                    return NotFound("Người dùng không tồn tại.");
                }

                if (user.Status == "Active")
                {
                    return Ok("Email đã được xác nhận trước đó.");
                }

                user.Status = "Active";
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return Ok("Xác nhận email thành công!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi xác nhận email: {ex.Message}");
            }
        }


    }
}
