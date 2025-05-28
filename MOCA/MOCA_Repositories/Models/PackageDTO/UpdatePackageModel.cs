using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Models.PackageDTO
{
    public class UpdatePackageModel
    {
        public string PackageName { get; set; }

        public string Status = "Active";
    }
}
