using MOCA_Repositories.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Interfaces
{
    public interface IBookingPaymentRepository
    {
        Task<BookingPayment> ConfirmPayment(int bookingId);


    }
}
