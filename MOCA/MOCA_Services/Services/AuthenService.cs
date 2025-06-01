using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.Login;
using MOCA_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.Services
{
    public class AuthenService : IAuthenService
    {
       private readonly IAuthenRepository _authenRepository;
        private readonly IEmailService _emailService;

        public AuthenService(IAuthenRepository authenRepository, IEmailService emailService)
        {
            _authenRepository = authenRepository;
            _emailService = emailService;   
        }

        public Task<string> Login(LoginModel model)
        {
            return _authenRepository.Login(model);

        }

        public async Task<User> Register(RegisterLoginModel newUser)
        {
            var user = await _authenRepository.Register(newUser);

            string confirmationLink = $"https://localhost:7066/api/Authen/verify-email?email={user.Email}";
            string body = $"<h3>Chào {user.FullName}</h3><p>Vui lòng xác nhận email của bạn bằng cách nhấn vào liên kết sau: <a href='{confirmationLink}'>Xác nhận email</a></p>";

            await _emailService.SendEmailAsync(user.Email, "Xác nhận Email", body);

            return user;
        }
    }
}
