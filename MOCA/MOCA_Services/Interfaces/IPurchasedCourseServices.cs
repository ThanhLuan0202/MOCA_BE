using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.Interfaces
{
    public interface IPurchasedCourseServices
    {
        Task<List<int>> GetEnrolledCourseIdsByUserNameAsync(int userId);
    }
}
