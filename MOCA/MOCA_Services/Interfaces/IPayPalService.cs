using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.Interfaces
{
    public interface IPayPalService
    {
        Task<string> CreatePaymentUrl(decimal amount, string returnUrl, string cancelUrl);
    }
}
