using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
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
        private readonly IConfiguration configuration;
        private readonly IMapper _mapper;

        public AuthenRepository(MOCAContext DbContext, IConfiguration configuration, IMapper mapper) : base(DbContext)
        {
            _DbContext = DbContext;
            this.configuration = configuration;
        }
       

        public async Task<string> Login(LoginModel model)
        {
            var user = await
                Entities.Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Status == "Active" && x.Email.ToLower().Equals(model.Email));

            if (user == null)
            {
                throw new Exception($"User not existed !!");
            }

            if (model.Password != user.Password)
            {
                throw new Exception($"Password wrong !!");
            }

            var claims = new[]
            {
        new Claim(ClaimTypes.Name, user.FullName),
        new Claim(ClaimTypes.Role, user.Role.RoleName),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim("Image", user.Image != null ? user.Image.ToString() : string.Empty)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Task<User> Register(User newUser)
        {
            throw new NotImplementedException();
        }
    }
}
