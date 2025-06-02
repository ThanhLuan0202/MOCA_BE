using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Models.DiscountDTO
{
    public class CreateDiscountModel
    {
        public string? Code { get; set; }

        public string? Description { get; set; }

        public decimal? Value { get; set; }

        public int? MaxUsage { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
