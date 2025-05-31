using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;

namespace MOCA_Repositories.Repositories
{
    public class PurchasedCourseRepository : Repository<PurchasedCourse>, IPurchasedCourseRepository
    {
        private readonly MOCAContext _dbcontext;
        public PurchasedCourseRepository(MOCAContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }


        public async Task<List<int>> GetEnrolledCourseIdsByUserNameAsync(int userId)
        {
            var courseIds = await Entities
                                  .Where(pc => pc.UserId == userId &&
                                               (pc.Status.ToLower() == "success" || pc.Status.ToLower() == "completed"))
                                  .Include(x => x.User)
                                  .Select(pc => pc.CourseId)
                                  .Where(id => id.HasValue) // Bỏ null
                                  .Select(id => id.Value)   // Ép về int
                                  .ToListAsync();

            return courseIds;
        }



    }
}
