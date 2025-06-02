using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Helper;
using MOCA_Services.Interfaces;

namespace MOCA_Services.Services
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _config;
        private readonly MOCAContext _context;
        public VnPayService(IConfiguration config, MOCAContext context)
        {
            _config = config;
            _context = context;
        }

        public string CreatePaymentUrl(PurchasedCourse course, HttpContext context)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);

            var vnp_Url = _config["Vnpay:PaymentUrl"];
            var vnp_ReturnUrl = _config["Vnpay:ReturnUrl"];
            var vnp_TmnCode = _config["Vnpay:TmnCode"];
            var vnp_HashSecret = _config["Vnpay:HashSecret"];

            var orderId = course.PurchasedId.ToString();
            var createDate = date.ToString("yyyyMMddHHmmss");

            var courseEntity = _context.Courses.FirstOrDefault(c => c.CourseId == course.CourseId);
            if (courseEntity == null)
                throw new Exception("Course does not exist");

            var amount = (int)(courseEntity.Price * 100); 

            var vnp_Params = new Dictionary<string, string>
    {
        { "vnp_Version", "2.1.0" },
        { "vnp_Command", "pay" },
        { "vnp_TmnCode", vnp_TmnCode },
        { "vnp_Amount", amount.ToString() },
        { "vnp_CreateDate", createDate },
        { "vnp_CurrCode", "VND" },
        { "vnp_IpAddr", context.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1" },
        { "vnp_Locale", "vn" },
        { "vnp_OrderInfo", $"Thanh toan khoa hoc {course.CourseId}" },
        { "vnp_OrderType", "other" },
        { "vnp_ReturnUrl", vnp_ReturnUrl },
        { "vnp_TxnRef", orderId }
    };

            var queryUrl = VnPayHelper.CreateRequestUrl(vnp_Params, vnp_HashSecret);
            return vnp_Url + "?" + queryUrl;
        }

    }
}
