using MOCA_Repositories.Enitities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOCA_Services.Interfaces
{
    public interface IDoctorBookingService
    {
        Task<(DoctorBooking booking, string? checkoutUrl)> CreateDoctorBooking(DoctorBooking doctorBooking, string userId);
        Task<DoctorBooking> ConfirmDoctorBooking(int id);
        Task<DoctorBooking> CancelDoctorBooking(int id);
        Task<DoctorBooking> BookingEnd(int id);
        Task<DoctorBooking> GettDoctorBookingById(int id);
        Task<IEnumerable<DoctorBooking>> GettAllDoctorBookingByDoctorId(string userId);
        Task<IEnumerable<DoctorBooking>> GettAllDoctorBookingByUserId(string userId);
        Task<DoctorBooking> GetBookingByUserId(int id);
    }
}
