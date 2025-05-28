using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.PackageDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Interfaces
{
    public interface IPacakgeRepository
    {

        Task<IEnumerable<Package>> GetAlLPackageAsync();
        Task<Package> CreatePackageAsync(CreatePackageModel newPackage);
        Task<Package> UpdatePackageAsync(int id, UpdatePackageModel updatePackage);
        Task<Package> DeletePackageAsync(int id);

        Task<Package> GetPackageByIdAsync(int id);
    }
}
