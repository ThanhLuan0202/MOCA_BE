using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Generic;
using MOCA_Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<DoctorBooking> CreateDoctorBooking(DoctorBooking doctorBooking, string userId)
        {
            if (!int.TryParse(userId, out int idUser))
                throw new ArgumentException("User ID không hợp lệ");

            var newBooking = new DoctorBooking
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

            await _context.DoctorBookings.AddAsync(newBooking);
            await _context.SaveChangesAsync();

            
                //var payment = new BookingPayment
                //{
                //    BookingId = newBooking.BookingId,
                //    Amount = newBooking.Price,
                //    PaymentMethod = "payOS",
                //    PaymentType = "Booking",
                //    IsPaid = false,
                //    CreateDate = DateTime.Now,
                //    PaypalOrderId = null 
                //};

                //await _context.BookingPayments.AddAsync(payment);
                //await _context.SaveChangesAsync();

                //newBooking.BookingPayments = new List<BookingPayment> { payment };
            

            return newBooking;
        }

        public async Task<DoctorBooking> ConfirmDoctorBooking(int id)
        {
            var booking = await _context.DoctorBookings
                .Include(x => x.Doctor)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.BookingId == id && x.Status == "Pending");

            if (booking == null)
                throw new Exception("Không tìm thấy lịch khám để xác nhận");

            booking.Status = "Confirm";
            _context.Entry(booking).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return booking;
        }

        public async Task<DoctorBooking> CancelDoctorBooking(int id)
        {
            var booking = await _context.DoctorBookings
                .Include(x => x.Doctor)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.BookingId == id);

            if (booking == null)
                throw new Exception("Không tìm thấy lịch khám để huỷ");

            booking.Status = "Inactive";
            await _context.SaveChangesAsync();

            return booking;
        }

        public async Task<DoctorBooking> BookingEnd(int id)
        {
            var booking = await _context.DoctorBookings
                .Include(x => x.Doctor)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.BookingId == id);

            if (booking == null)
                throw new Exception("Không tìm thấy lịch khám để hoàn tất");

            booking.Status = "Complete";
            await _context.SaveChangesAsync();

            return booking;
        }

        public async Task<IEnumerable<DoctorBooking>> GettAllDoctorBookingByDoctorId(string userId)
        {
            var doctor = await _doctorProfileRepository.GetDoctorProfileByUserIdAsync(userId);
            if (doctor == null) return Enumerable.Empty<DoctorBooking>();

            return await _context.DoctorBookings
                .Include(x => x.User)
                .Include(x => x.Doctor)
                .Where(x => x.DoctorId == doctor.DoctorId && (x.Status == "Confirm" || x.Status == "Complete"))
                .ToListAsync();
        }

        public async Task<IEnumerable<DoctorBooking>> GettAllDoctorBookingByUserId(string userId)
        {
            if (!int.TryParse(userId, out int uid)) return Enumerable.Empty<DoctorBooking>();

            return await _context.DoctorBookings
                .Include(x => x.Doctor)
                .Include(x => x.User)
                .Where(x => x.UserId == uid && (x.Status == "Confirm" || x.Status == "Complete"))
                .ToListAsync();
        }

        public async Task<DoctorBooking> GettDoctorBookingById(int id)
        {
            var booking = await _context.DoctorBookings
                .Include(x => x.User)
                .Include(x => x.Doctor)
                .FirstOrDefaultAsync(x => x.BookingId == id);

            if (booking == null) throw new Exception("Không tìm thấy lịch khám");

            return booking;
        }

        public async Task<DoctorBooking> GetBookingByUserId(int id)
        {
           var checkPay = await _context.DoctorBookings.Include(x => x.User).Include(c => c.BookingPayments).FirstOrDefaultAsync(x => x.UserId == id && x.Status.ToLower().Equals("Pending"));

            if (checkPay == null)
            {
                throw new Exception($"Booking {checkPay} is not exist!");
            }
            return checkPay;
        }
    }
}
