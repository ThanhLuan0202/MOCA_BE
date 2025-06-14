using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Services.Interfaces;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MOCA_Services.Services
{
    public class DoctorBookingService : IDoctorBookingService
    {
        private readonly IDoctorBookingRepository _repo;
        private readonly IPayPalService _payPalService;
        private readonly IConfiguration _config;
        public DoctorBookingService(IDoctorBookingRepository repo, IPayPalService payPalService, IConfiguration config)
        {
            _repo = repo;
            _payPalService = payPalService;
            _config = config;
        }

        public Task<DoctorBooking> BookingEnd(int id)
        {
            return _repo.BookingEnd(id);
        }

        public Task<DoctorBooking> CancelDoctorBooking(int id)
        {
           return _repo.CancelDoctorBooking(id);
        }

        public Task<DoctorBooking> ConfirmDoctorBooking(int id)
        {
            return _repo.ConfirmDoctorBooking(id);
        }

        public async Task<(DoctorBooking booking , string? paymentUrl)> CreateDoctorBooking(DoctorBooking doctorBooking, string userId)
        {
            var booking = await _repo.CreateDoctorBooking(doctorBooking, userId);

            string? paymentUrl = null;

            if (booking.RequiredDeposit.HasValue && booking.RequiredDeposit.Value > 0)
            {
                paymentUrl = await _payPalService.CreatePaymentUrl(
                    booking.RequiredDeposit.Value,
                    _config["PayPal:ReturnUrl"],
                    _config["PayPal:CancelUrl"]
                );
            }




            return (booking, paymentUrl);

        }

        public Task<IEnumerable<DoctorBooking>> GettAllDoctorBookingByDoctorId(string userId)
        {
            return _repo.GettAllDoctorBookingByDoctorId(userId);
        }

        public Task<IEnumerable<DoctorBooking>> GettAllDoctorBookingByUserId(string userId)
        {
            return _repo.GettAllDoctorBookingByUserId(userId);
        }

        public Task<DoctorBooking> GettDoctorBookingById(int id)
        {
            return _repo.GettDoctorBookingById(id);
        }
    }
}
