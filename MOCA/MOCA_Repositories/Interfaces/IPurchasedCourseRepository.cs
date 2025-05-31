using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;

namespace MOCA_Repositories.Interfaces
{
    public interface IPurchasedCourseRepository
    {
        Task<List<int>> GetEnrolledCourseIdsByUserNameAsync(int userId);
    }
}
