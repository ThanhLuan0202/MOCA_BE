using MOCA_Repositories.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Interfaces
{
    public interface IDoctorBookingRepository
    {
        Task<IEnumerable<DoctorBooking>> GettAllDoctorBookingByUserId(string userId);
        Task<IEnumerable<DoctorBooking>> GettAllDoctorBookingByDoctorId(string userId);
        Task<DoctorBooking> GettDoctorBookingById(int id);

        Task<DoctorBooking> CreateDoctorBooking(DoctorBooking doctorBooking, string userId);
        Task<DoctorBooking> BookingEnd(int id);
        Task<DoctorBooking> CancelDoctorBooking(int id);

        Task<DoctorBooking> ConfirmDoctorBooking(int id);




    }
}
