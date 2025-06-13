using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Services.Interfaces;

namespace MOCA_Services.Services
{
    public class CoursePaymentService : ICoursePaymentService
    {
        private readonly ICoursePaymentRepository _coursePaymentRepository;
        public CoursePaymentService(ICoursePaymentRepository coursePaymentRepository)
        {
            _coursePaymentRepository = coursePaymentRepository;
        }
        public async Task CreateFromVnPayAsync(OrderCourse order, IQueryCollection vnpQuery, string status)
        {
            await _coursePaymentRepository.CreateFromVnPayAsync(order, vnpQuery, status);
        }
    }
}
