using Microsoft.EntityFrameworkCore;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Generic;
using MOCA_Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Repositories
{
    public class DoctorBookingRepository : GenericRepository<DoctorBooking>, IDoctorBookingRepository
    {
        private readonly MOCAContext _context;
        private readonly IDoctorProfileRepository _doctorProfileRepository;

        public DoctorBookingRepository(MOCAContext context, IDoctorProfileRepository doctorProfileRepository)
        {
            _context = context;
            _doctorProfileRepository = doctorProfileRepository;
        }

        public async Task<DoctorBooking> BookingEnd(int id)
        {
            var check = await _context.DoctorBookings.Include(x => x.Doctor).Include(c => c.User).FirstOrDefaultAsync(b => b.BookingId == id);

            if (check == null)
            {
                throw new Exception($"Doctor Booking {id} is not exist!");
            }

            check.Status = "Complete";

            _context.Entry(check).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return check;
        }

        public async Task<DoctorBooking> CancelDoctorBooking(int id)
        {
            var check = await _context.DoctorBookings.Include(x => x.Doctor).Include(c => c.User).FirstOrDefaultAsync(b => b.BookingId == id);

            if (check == null)
            {
                throw new Exception($"Doctor Booking {id} is not exist!");
            }
            check.Status = "Inactive";
            _context.Entry(check).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return check;
        }

        public async Task<DoctorBooking> ConfirmDoctorBooking(int id)
        {
            var check = await _context.DoctorBookings.Include(x => x.Doctor).Include(c => c.User).FirstOrDefaultAsync(b => b.BookingId == id && b.Status == "Pending");

            if (check == null)
            {
                throw new Exception($"Doctor Booking {id} is not exist!");
            }
            check.Status = "Confirm";
            _context.Entry(check).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return check;
        }

        public async Task<DoctorBooking> CreateDoctorBooking(DoctorBooking doctorBooking, string userId)
        {
            if (!int.TryParse(userId, out int idUser))
            {
                throw new ArgumentException("User is Invalid!");
            }

            var newDoctorBooking = new DoctorBooking
            {
                UserId = idUser,
                DoctorId = doctorBooking.DoctorId,
                BookingDate = doctorBooking.BookingDate,
                ConsultationType = doctorBooking.ConsultationType,
                RequiredDeposit = doctorBooking.RequiredDeposit,
                Status = "Pending",
                Notes = doctorBooking.Notes,
                Price = doctorBooking.Price
            };

            await _context.AddAsync(newDoctorBooking);
            await _context.SaveChangesAsync();
            return newDoctorBooking;
        }

        public async Task<IEnumerable<DoctorBooking>> GettAllDoctorBookingByDoctorId(string userId)
        {
            var doctor = await _doctorProfileRepository.GetDoctorProfileByUserIdAsync(userId);


            var query = await _context.DoctorBookings.Include(x => x.Doctor).Include(c => c.User).Where(b => b.DoctorId == doctor.DoctorId && b.Status == "Confirm" || b.Status == "Complete").ToListAsync();

            return query;

        }

        public async Task<IEnumerable<DoctorBooking>> GettAllDoctorBookingByUserId(string userId)
        {
            if (!int.TryParse(userId, out int idUser))
            {
                throw new ArgumentException("Invalid user ID");

            }

            var query = await _context.DoctorBookings.Include(x => x.User).Include(y => y.Doctor).Where(x => x.UserId == idUser && x.Status == "Confirm").ToListAsync();

            return query;
        }

        public async Task<DoctorBooking> GettDoctorBookingById(int id)
        {
            var check = await _context.DoctorBookings.Include(x => x.Doctor).Include(c => c.User).FirstOrDefaultAsync(b => b.BookingId == id);

            if (check == null)
            {
                throw new Exception($"Doctor Booking {id} is not exist!");
            }

            return check;
        }
    }
}
