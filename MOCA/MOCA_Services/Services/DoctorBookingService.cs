using Microsoft.Extensions.Configuration;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Services.Interfaces;

namespace MOCA_Services.Services
{
    public class DoctorBookingService : IDoctorBookingService
    {
        private readonly IDoctorBookingRepository _bookingRepo;
        private readonly IConfiguration _config;
        private readonly MOCAContext _context;
      
        public DoctorBookingService(
            IDoctorBookingRepository bookingRepo,
            IConfiguration config,
            MOCAContext context
           )
        {
            _bookingRepo = bookingRepo;
            _config = config;
            _context = context;
           
        }

        public async Task<(DoctorBooking booking, string? checkoutUrl)> CreateDoctorBooking(DoctorBooking doctorBooking, string userId)
        {
            if (!int.TryParse(userId, out int userIdInt))
                throw new Exception("Invalid user ID");

            // 1. Tạo booking & payment tạm
            var booking = await _bookingRepo.CreateDoctorBooking(doctorBooking, userId);
            var payment = booking.BookingPayments?.FirstOrDefault(p => p.IsPaid == false);

            if (booking.RequiredDeposit > 0 && payment != null)
            {
                // Gọi PayOSService chuẩn hóa
                // Đã lưu paymentId vào payment.PaypalOrderId trong service
            }

            return (booking, null);
        }




        public Task<DoctorBooking> BookingEnd(int id)
        {
            return _bookingRepo.BookingEnd(id);
        }

        public Task<DoctorBooking> CancelDoctorBooking(int id)
        {
            return _bookingRepo.CancelDoctorBooking(id);
        }

        public Task<DoctorBooking> ConfirmDoctorBooking(int id)
        {
            return _bookingRepo.ConfirmDoctorBooking(id);
        }

        public Task<DoctorBooking> GettDoctorBookingById(int id)
        {
            return _bookingRepo.GettDoctorBookingById(id);
        }

        public Task<IEnumerable<DoctorBooking>> GettAllDoctorBookingByDoctorId(string userId)
        {
            return _bookingRepo.GettAllDoctorBookingByDoctorId(userId);
        }

        public Task<IEnumerable<DoctorBooking>> GettAllDoctorBookingByUserId(string userId)
        {
            return _bookingRepo.GettAllDoctorBookingByUserId(userId);
        }

        public Task<DoctorBooking> GetBookingByUserId(int id)
        {
            return _bookingRepo.GetBookingByUserId(id);
        }

        
    }
}
