using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Models.PackageDTO
{
    public class PurchaseRequest
    {
        public int PackageId { get; set; }
        public decimal Amount { get; set; }
    }
}
