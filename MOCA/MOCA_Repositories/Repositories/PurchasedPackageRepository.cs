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
    public class PurchasedPackageRepository : GenericRepository<PurchasePackage>, IPurchasedPackageRepository
    {
        private readonly MOCAContext _context;



        public PurchasedPackageRepository(MOCAContext context)
        {
            _context = context;
        }

        public async Task<PurchasePackage> AddAsync(string userId, PurchasePackage create)
        {
            if (!int.TryParse(userId, out int idUser))
            {
                throw new ArgumentException("Invalid user ID");
            }
            var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);

            var newPc = new PurchasePackage
            {
                PackageId = create.PackageId,
                UserId = idUser,
                PurchaseDate = vietnamTime,
                Status = "Active",
                DiscountId = create.DiscountId,
            };


            await _context.AddAsync(newPc);
            await _context.SaveChangesAsync();

            return newPc;
        }

        public async Task<PurchasePackage> DeleteAsync(int id)
        {
            var checkPurPc = await _context.PurchasePackages.FirstOrDefaultAsync(x => x.PurchasePackageId == id);

            if (checkPurPc == null)
            {
                throw new Exception($"Purchase Package {id} is not exist !");
            }

            checkPurPc.Status = "Inactive";

            _context.Entry(checkPurPc).State = EntityState.Modified; 

            await _context.SaveChangesAsync();
            
            return checkPurPc;
        }

        public async Task<List<PurchasePackage>> GetAllAsync()
        {
            var query = await _context.PurchasePackages.Include(x => x.Package).ToListAsync();

            return query;
        }

        public async Task<PurchasePackage?> GetByIdAsync(int id)
        {
            var checkPurPc = await _context.PurchasePackages.FirstOrDefaultAsync(x => x.PurchasePackageId == id);

            if (checkPurPc == null)
            {
                throw new Exception($"Purchase Package {id} is not exist !");
            }

            return checkPurPc;
        }

        public async Task<List<int>> GetEnrolledPurchasePackageIdsByUserNameAsync(string userId)
        {
            if (!int.TryParse(userId, out int idUser)) 
            {
                throw new ArgumentException("Invalid user ID");
            }
            var courseIds = await _context.PurchasePackages
                                 .Where(pc => pc.UserId == idUser &&
                                              (pc.Status.ToLower() == "Active"))
                                 .Include(x => x.User)
                                 .Select(pc => pc.PurchasePackageId)
                                 .ToListAsync();

            return courseIds;
        }

        
    }
}
