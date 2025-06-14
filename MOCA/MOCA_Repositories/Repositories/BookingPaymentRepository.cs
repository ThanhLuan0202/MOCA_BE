using Microsoft.EntityFrameworkCore;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Repositories
{
    public class BookingPaymentRepository : IBookingPaymentRepository
    {
        private readonly MOCAContext _context;
        public BookingPaymentRepository(MOCAContext context)
        {
            _context = context;
        }
        public async Task<BookingPayment> ConfirmPayment(int id)
        {
            var checkPm = await _context.BookingPayments.Include(x => x.Booking).FirstOrDefaultAsync(c => c.PaymentId == id);

            if (checkPm == null)
            {
                throw new Exception($"Payment {id} not found");
            }
            return checkPm;
        }
    }
}
