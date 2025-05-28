using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.Interfaces
{
    public interface IAuthenService
    {
        Task<string> Login(LoginModel model);
        Task<User> Register(RegisterLoginModel newUser);
    }
}
