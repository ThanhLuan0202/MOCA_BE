using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Models.PayOS
{
    public class PayOSRequestDto
    {
        public long orderCode { get; set; }
        public long amount { get; set; }
        public string description { get; set; }
        public string returnUrl { get; set; }
        public string cancelUrl { get; set; }
        public List<Item> items { get; set; }  // <-- thêm dòng này
    }

    public class Item
    {
        public string name { get; set; }
        public int quantity { get; set; }
        public long price { get; set; }
    }

}
