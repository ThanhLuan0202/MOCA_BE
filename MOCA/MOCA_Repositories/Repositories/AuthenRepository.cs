using AutoMapper;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Generic;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models;
using MOCA_Repositories.Models.Login;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Repositories
{
    public class AuthenRepository : Repository<User>, IAuthenRepository
    {
        private readonly MOCAContext _DbContext;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthenRepository(MOCAContext DbContext, IConfiguration configuration, IMapper mapper) : base(DbContext)
        {
            _DbContext = DbContext;
            _configuration = configuration;
        }


        public async Task<string> Login(LoginModel model)
        {
            var user = await
                Entities.Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(model.Email) && x.Status == "Active");

            if (user == null)
            {
                throw new Exception($"User not existed !!");
            }

            if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                throw new Exception($"Password wrong !!");
            }

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        new Claim(ClaimTypes.Name, user.FullName),
        new Claim(ClaimTypes.Role, user.Role.RoleName),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim("Image", user.Image != null ? user.Image.ToString() : string.Empty)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User> Register(RegisterLoginModel user)
        {
            try
            {
                var existUser = await Entities
                    .FirstOrDefaultAsync(x => x.Email == user.Email || x.PhoneNumber == user.PhoneNumber);

                if (existUser != null)
                {
                    if (existUser.Email == user.Email)
                    {
                        throw new Exception($"Email: {user.Email} is exist !");
                    }

                    if (existUser.PhoneNumber == user.PhoneNumber)
                    {
                        throw new Exception($"Phone number: {user.PhoneNumber} is exist !");
                    }
                }
                string password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                var newUser = new User
                {

                    RoleId = user.RoleId,
                    FullName = user.FullName,
                    Email = user.Email,
                    Password = password,
                    PhoneNumber = user.PhoneNumber,
                    Status = "Acitve"
                };


                if (user.Image != null)
                {
                    var imageUrl = await UploadFileAsyncc(user.Image);
                    newUser.Image = imageUrl;
                }
                await Entities.AddAsync(newUser);
                await _DbContext.SaveChangesAsync();

                return newUser;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi đăng ký: {ex.Message}");
            }


        }
        public async Task<string> UploadFileAsyncc(IFormFile file)
        {
            if (file.Length > 0)
            {
                var stream = file.OpenReadStream();
                var bucket = _configuration["FireBase:Bucket"];

                var task = new FirebaseStorage(bucket)
                    .Child("Image_Course")
                    .Child(file.FileName)
                    .PutAsync(stream);

                var downloadUrl = await task;
                return downloadUrl;
            }
            return null;
        }
    }
}
