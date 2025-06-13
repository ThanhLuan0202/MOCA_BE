using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Search;
using Microsoft.AspNetCore.Http;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.CourseCartDTO;

namespace MOCA_Repositories.Repositories
{

    public class CoursePaymentRepository : ICoursePaymentRepository
    {
        private readonly MOCAContext _context;
        public CoursePaymentRepository(MOCAContext context)
        {
            _context = context;
        }

        public async Task CreateFromVnPayAsync(OrderCourse order, IQueryCollection vnpQuery, string status)
        {
            var vnp_txnref = vnpQuery["vnp_TxnRef"].ToString();
            var vnp_transactionNo = vnpQuery["vnp_TransactionNo"].ToString();
            var vnp_amountStr = vnpQuery["vnp_Amount"].ToString();

            double.TryParse(vnp_amountStr, out double amt);
            double amount = amt / 100;

            var coursePayment = new CoursePayment
            {
                OrderId = order.OrderId,
                PaymentCode = vnp_txnref,
                TransactionIdResponse = vnp_transactionNo,
                PaymentGateway = "VNPay",
                CreatedDate = DateTime.Now,
                Amount = (double?)amount,
                Description = $"Invoice #{order.OrderId} has been successfully paid.",
                Status = status,
            };

            _context.CoursePayments.Add(coursePayment);
            await _context.SaveChangesAsync();
        }
    }
}
