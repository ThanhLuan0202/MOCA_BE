using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.Services
{
    public class BookingPaymentService : IBookingPaymentService
    {
        private readonly IBookingPaymentRepository _bookingPaymentRepository;   
        public BookingPaymentService(IBookingPaymentRepository bookingPaymentRepository)
        {
            _bookingPaymentRepository = bookingPaymentRepository;
        }
        public Task<BookingPayment> ConfirmPayment(int bookingId)
        {
            return _bookingPaymentRepository.ConfirmPayment(bookingId);
        }
    }
}
