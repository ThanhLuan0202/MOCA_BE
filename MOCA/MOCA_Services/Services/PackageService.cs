using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.PackageDTO;
using MOCA_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.Services
{
    public class PackageService : IPackageService
    {
        private readonly IPacakgeRepository _packageRepository;
        public PackageService(IPacakgeRepository packageRepository)
        {
                _packageRepository = packageRepository;   
        }
        public Task<Package> CreatePackageAsync(CreatePackageModel newPackage)
        {
            return _packageRepository.CreatePackageAsync(newPackage);
        }

        public Task<Package> DeletePackageAsync(int id)
        {
            return _packageRepository.DeletePackageAsync(id);
        }

        public Task<IEnumerable<Package>> GetAlLPackageAsync()
        {
            return _packageRepository.GetAlLPackageAsync();
        }

        public Task<Package> GetPackageByIdAsync(int id)
        {
            return _packageRepository.GetPackageByIdAsync(id);
        }

        public Task<Package> UpdatePackageAsync(int id, UpdatePackageModel updatePackage)
        {
            return _packageRepository.UpdatePackageAsync(id, updatePackage);
        }
    }
}
