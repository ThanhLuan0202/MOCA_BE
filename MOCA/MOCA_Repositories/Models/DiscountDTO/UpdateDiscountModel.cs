using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enums;

namespace MOCA_Repositories.Models.DiscountDTO
{
    public class UpdateDiscountModel
    {
        public string? Code { get; set; }

        public string? Description { get; set; }

        public DiscountType? DiscountType { get; set; }

        public decimal? Value { get; set; }

        public int? MaxUsage { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
