using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MOCA_Repositories.Enitities;

namespace MOCA_Repositories.Interfaces
{
    public interface ICoursePaymentRepository
    {
        Task CreateFromVnPayAsync(OrderCourse order, IQueryCollection vnpQuery, string status);
    }
}
