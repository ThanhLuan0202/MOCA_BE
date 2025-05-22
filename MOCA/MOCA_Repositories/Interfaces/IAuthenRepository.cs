using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models;
using MOCA_Repositories.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Interfaces
{
    public interface IAuthenRepository
    {
        Task<string> Login(LoginModel model);
        Task<User> Register(User newUser);
    }
}
