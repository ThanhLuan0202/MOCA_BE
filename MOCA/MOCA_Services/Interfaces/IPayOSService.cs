using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.Interfaces
{
    public interface IPayOSService
    {
        Task<CreatePaymentResult> CreatePaymentUrl(int orderId);

        WebhookData VerifyWebhook(WebhookType webhook);
        Task<PaymentLinkInformation> GetPaymentInfo(int orderId);
        Task<PaymentLinkInformation> CancelOrder(string orderId, string reason);
    }
}
