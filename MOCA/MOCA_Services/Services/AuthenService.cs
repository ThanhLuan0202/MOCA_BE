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

        public AuthenService(IAuthenRepository authenRepository)
        {
            _authenRepository = authenRepository;
        }

        public Task<string> Login(LoginModel model)
        {
            return _authenRepository.Login(model);

        }

        public Task<User> Register(User newUser)
        {
            throw new NotImplementedException();
        }
    }
}
