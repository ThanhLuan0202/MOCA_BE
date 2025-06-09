using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Services.Interfaces;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.Services
{
    public class DoctorBookingService : IDoctorBookingService
    {
        private readonly IDoctorBookingRepository _repo;

        public DoctorBookingService(IDoctorBookingRepository repo)
        {
            _repo = repo;
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

        public Task<DoctorBooking> CreateDoctorBooking(DoctorBooking doctorBooking, string userId)
        {
            return _repo.CreateDoctorBooking(doctorBooking, userId);
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
