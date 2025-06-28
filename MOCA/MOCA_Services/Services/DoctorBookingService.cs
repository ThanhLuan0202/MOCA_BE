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
        private readonly IPayPalService _payPalService;

        public DoctorBookingService(
            IDoctorBookingRepository bookingRepo,
            IConfiguration config,
            MOCAContext context,
            IPayPalService payPalService)
        {
            _bookingRepo = bookingRepo;
            _config = config;
            _context = context;
            _payPalService = payPalService;
            
        }

        public async Task<(DoctorBooking booking, string? paymentUrl)> CreateDoctorBooking(DoctorBooking doctorBooking, string userId)
        {

            if (!int.TryParse(userId, out int idUser))
            {
                throw new Exception($"userId {userId} is invalid!");
            }
            var booking = await _bookingRepo.CreateDoctorBooking(doctorBooking, userId);
            string? paymentUrl = null;
            var payment = booking.BookingPayments?.FirstOrDefault(x => x.IsPaid == false);

            if (booking.RequiredDeposit > 0 && payment != null)
            {
                var returnUrl = $"{_config["PayPal:ReturnUrl"]}?paymentId={payment.PaymentId}";
                var cancelUrl = $"{_config["PayPal:CancelUrl"]}?paymentId={payment.PaymentId}";

                var (paypalOrderId, url) = await _payPalService.CreatePaymentWithOrderId(
                    booking.RequiredDeposit.Value,
                    returnUrl,
                    cancelUrl
                );

                // Cập nhật lại PaypalOrderId cho payment
                payment.PaypalOrderId = paypalOrderId;
                await _context.SaveChangesAsync();

                paymentUrl = url;
            }

            return (booking, paymentUrl);
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
