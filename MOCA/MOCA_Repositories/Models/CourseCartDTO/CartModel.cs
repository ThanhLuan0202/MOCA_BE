using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Models.CourseCartDTO
{
    public class CartModel
    {
        public int UserId { get; set; }
        public int? DiscountId { get; set; }
        public decimal DiscountValue { get; set; } = 0;
        public List<CartItemModel> Items { get; set; } = new();
    }
}
