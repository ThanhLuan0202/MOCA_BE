using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.CourseCategoryDTO;
using MOCA_Repositories.Models.FeedbackDTO;

namespace MOCA_Repositories.Repositories
{
    public class CourseCategoryRepository : Repository<CourseCategory>, ICourseCategoryRepository
    {
        private readonly MOCAContext _context;
        public CourseCategoryRepository(MOCAContext context) : base(context) 
        {
            _context = context;
        }
        public async Task<CourseCategory> AddAsync(CreateCourseCategoryModel create)
        {
            if (create == null)
            {
                throw new ArgumentNullException(nameof(create));
            }

            var exists = await _context.CourseCategories.AnyAsync(x => x.Name == create.Name);
            if (exists)
            {
                throw new InvalidOperationException("Category with the same name already exists.");
            }
            var newCategory = new CourseCategory
            {
                Name = create.Name,
            };

            _context.CourseCategories.Add(newCategory);
            await _context.SaveChangesAsync();
            return newCategory;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.CourseCategories.FindAsync(id);
            if (category == null) return false;

            _context.CourseCategories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CourseCategory>> GetAllAsync()
        {
            return await _context.CourseCategories.ToListAsync();
        }

        public async Task<CourseCategory?> GetByIdAsync(int id)
        {
            return await _context.CourseCategories.FindAsync(id);
        }

        public async Task<CourseCategory> UpdateAsync(int id, UpdateCourseCategoryModel update)
        {
            var existing = await _context.CourseCategories.FindAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Feedback with id {id} not found");

            var duplicate = await _context.CourseCategories
                .AnyAsync(c => c.Name == update.Name && c.CategoryId != id);

            if (duplicate)
            {
                throw new InvalidOperationException("Another category with the same name already exists.");
            }

            existing.Name = update.Name;
            _context.CourseCategories.Update(existing);
            await _context.SaveChangesAsync();

            return existing;
        }
    }
}
