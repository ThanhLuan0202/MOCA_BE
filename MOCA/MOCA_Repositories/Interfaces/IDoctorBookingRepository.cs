using MOCA_Repositories.Enitities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOCA_Repositories.Interfaces
{
    public interface IDoctorBookingRepository
    {
        Task<DoctorBooking> CreateDoctorBooking(DoctorBooking doctorBooking, string userId);
        Task<DoctorBooking> ConfirmDoctorBooking(int id);
        Task<DoctorBooking> CancelDoctorBooking(int id);
        Task<DoctorBooking> BookingEnd(int id);
        Task<IEnumerable<DoctorBooking>> GettAllDoctorBookingByUserId(string userId);
        Task<IEnumerable<DoctorBooking>> GettAllDoctorBookingByDoctorId(string userId);
        Task<DoctorBooking> GettDoctorBookingById(int id);
        Task<DoctorBooking> GetBookingByUserId(int id);

    }
}
