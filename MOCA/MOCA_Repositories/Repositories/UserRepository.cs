using Microsoft.EntityFrameworkCore;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Generic;
using MOCA_Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly MOCAContext _context;
        public UserRepository(MOCAContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
           var listUser = await _context.Users.ToListAsync();
            if (listUser == null || listUser.Count == 0)
            {
                throw new Exception("There is no user in the database!");
            }
            return listUser;
        }

        public async Task<User> GetUserById(int id)
        {
            var check = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);

            if (check == null)
            {
                throw new Exception($"User {id} is not exist!");
            }
            return check;
        }
    }
    
}
