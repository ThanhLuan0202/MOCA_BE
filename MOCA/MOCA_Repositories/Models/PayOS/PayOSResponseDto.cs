using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Models.PayOS
{
    public class PayOSResponseDto
    {
        public string checkoutUrl { get; set; }
        public long orderCode { get; set; }
    }
}
