using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Generic;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.PackageDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Repositories
{
    public class PackageRepository : GenericRepository<Package>, IPacakgeRepository
    {
        private readonly MOCAContext _context;
        private readonly IMapper _mapper;

        public PackageRepository(MOCAContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Package> CreatePackageAsync(CreatePackageModel newPackage)
        {
            var packageNew = new Package
            {
                PackageName = newPackage.PackageName,
                Status = newPackage.Status
            };


            await _context.Packages.AddAsync(packageNew);
            await _context.SaveChangesAsync();
            return packageNew;
        }

        public async Task<Package> DeletePackageAsync(int id)
        {
            var check = await _context.Packages.FirstOrDefaultAsync(x => x.PackageId == id);
            if (check == null)
            {
                throw new Exception($"Package {id} is not exist!");
            }
            check.Status = "Inactive";
            _context.Entry(check).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return check;

        }

        public async Task<IEnumerable<Package>> GetAlLPackageAsync()
        {
            var query = await _context.Packages.ToListAsync();

            return query;
        }

        public async Task<Package> GetPackageByIdAsync(int id)
        {
            var check = await _context.Packages.FirstOrDefaultAsync(x => x.PackageId == id);

            if (check == null)
            {
                throw new Exception($"Package {id} is not exist!");
            }

            return check;
        }

        public async Task<Package> UpdatePackageAsync(int id, UpdatePackageModel updatePackage)
        {
            var check = await _context.Packages.FirstOrDefaultAsync(x => x.PackageId == id);

            if (check == null)
            {
                throw new Exception($"Package {id} is not exist!");
            }
            check.PackageName = updatePackage.PackageName;


            _context.Entry(check).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return check;
        }
    }
}
