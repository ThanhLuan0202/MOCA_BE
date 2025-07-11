using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.Interfaces
{
    public interface IPayPackageService
    {

        Task<CreatePaymentResult> CreatePaymentUrl(int orderId);

        WebhookData VerifyWebhook(WebhookType webhook);
    }
}
