using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.PurchasedCourseDTO;

namespace MOCA_Repositories.Repositories
{
    public class PurchasedCourseRepository : Repository<PurchasedCourse>, IPurchasedCourseRepository
    {
        private readonly MOCAContext _context;
        public PurchasedCourseRepository(MOCAContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PurchasedCourse> AddAsync(int userId, CreatePurchasedCourseModel create)
        {
            if (create.CourseId.HasValue)
            {
                var courseExists = await _context.Courses
                    .AnyAsync(c => c.CourseId == create.CourseId.Value);

                if (!courseExists)
                {
                    throw new ArgumentException($"Course with ID {create.CourseId.Value} does not exist.");
                }
            }

            Discount? appliedDiscount = null;

            if (create.DiscountId.HasValue)
            {
                appliedDiscount = await _context.Discounts
                    .FirstOrDefaultAsync(d =>
                        d.DiscountId == create.DiscountId.Value &&
                        d.IsActive == true &&
                        d.MaxUsage > 0
                    );

                if (appliedDiscount == null)
                {
                    throw new ArgumentException($"Discount with ID {create.DiscountId.Value} is either invalid, inactive, or has no usage left.");
                }

                appliedDiscount.MaxUsage -= 1;
                _context.Discounts.Update(appliedDiscount);
            }

            var newPurchasedCourse = new PurchasedCourse
            {
                CourseId = create.CourseId,
                UserId = userId,
                //PurchaseDate = DateTime.UtcNow,
                Status = "Active",
                //DiscountId = create.DiscountId
            };

            _context.PurchasedCourses.Add(newPurchasedCourse);
            await _context.SaveChangesAsync();

            return newPurchasedCourse;
        }


        public async Task<PurchasedCourse> DeleteAsync(int id)
        {
            var purchasedCourse = await _context.PurchasedCourses.FindAsync(id);
            if (purchasedCourse == null)
                return null;

            if (purchasedCourse.Status == "Inactive")
                return purchasedCourse;

            purchasedCourse.Status = "Inactive";

            //if (purchasedCourse.DiscountId.HasValue)
            //{
            //    var discount = await _context.Discounts.FindAsync(purchasedCourse.DiscountId.Value);
            //    if (discount != null)
            //    {
            //        discount.MaxUsage += 1;
            //        _context.Discounts.Update(discount);
            //    }
            //}

            try
            {
                _context.PurchasedCourses.Update(purchasedCourse);
                var result = await _context.SaveChangesAsync();

                if (result == 0)
                    return null;

                return purchasedCourse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deactivating purchasedCourse: {ex.Message}");
                return null;
            }
        }


        public async Task<List<PurchasedCourse>> GetAllAsync()
        {
            return await _context.PurchasedCourses.ToListAsync();
        }

        public async Task<PurchasedCourse?> GetByIdAsync(int id)
        {
            return await _context.PurchasedCourses.FindAsync(id);
        }

        public async Task<List<int>> GetEnrolledCourseIdsByUserNameAsync(int userId)
        {
            var courseIds = await Entities
                                  .Where(pc => pc.UserId == userId &&
                                               (pc.Status.ToLower() == "success" || pc.Status.ToLower() == "completed"))
                                  .Include(x => x.User)
                                  .Select(pc => pc.CourseId)
                                  .Where(id => id.HasValue) // Bỏ null
                                  .Select(id => id.Value)   // Ép về int
                                  .ToListAsync();

            return courseIds;
        }

        public async Task<PurchasedCourse> UpdateAsync(int id, UpdatePurchasedCourseModel update)
        {
            var existing = await _context.PurchasedCourses.FindAsync(id);
            if (existing == null)
                return null;

            if (update.CourseId.HasValue && update.CourseId != existing.CourseId)
            {
                var courseExists = await _context.Courses.AnyAsync(c => c.CourseId == update.CourseId.Value);
                if (!courseExists)
                    throw new ArgumentException($"Course with ID {update.CourseId.Value} does not exist.");

                existing.CourseId = update.CourseId;
            }

            //if (update.DiscountId.HasValue && update.DiscountId != existing.DiscountId)
            //{
            //    if (existing.DiscountId.HasValue)
            //    {
            //        var oldDiscount = await _context.Discounts.FindAsync(existing.DiscountId.Value);
            //        if (oldDiscount != null)
            //        {
            //            oldDiscount.MaxUsage += 1;
            //            _context.Discounts.Update(oldDiscount);
            //        }
            //    }

            //    var newDiscount = await _context.Discounts
            //        .FirstOrDefaultAsync(d =>
            //            d.DiscountId == update.DiscountId.Value &&
            //            d.IsActive == true &&
            //            d.MaxUsage > 0);

            //    if (newDiscount == null)
            //        throw new ArgumentException($"Discount with ID {update.DiscountId.Value} is invalid, inactive, or out of usage.");

            //    newDiscount.MaxUsage -= 1;
            //    _context.Discounts.Update(newDiscount);

            //    existing.DiscountId = update.DiscountId;
            //}
            //existing.PurchaseDate = DateTime.UtcNow;
            existing.Status = "Active";

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> HasUserPurchasedCourse(int userId, int courseId)
        {
            return await _context.PurchasedCourses
                .AnyAsync(x => x.UserId == userId && x.CourseId == courseId && x.Status == "Completed");
        }

    }
}
