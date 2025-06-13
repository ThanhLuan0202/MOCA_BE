using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MOCA_Repositories.Enitities;

namespace MOCA_Services.Interfaces
{
    public interface ICourseVnPayService
    {
        string CreatePaymentUrl(OrderCourse order, HttpContext context);
        
    }
}
