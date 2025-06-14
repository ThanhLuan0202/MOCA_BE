using MOCA_Repositories.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.Interfaces
{
    public interface IBookingPaymentService
    {
        Task<BookingPayment> ConfirmPayment(int id);

    }
}
