using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.PayOS;
using MOCA_Services.Constants;
using MOCA_Services.Interfaces;
using Net.payOS;
using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.Services
{
    public class PayPackageService : IPayPackageService
    {
        private readonly PayOSSettings _payOSSettings;
        private readonly MOCAContext _context;

        public PayPackageService(IOptions<PayOSSettings> payOSSettings, MOCAContext context)
        {
            _payOSSettings = payOSSettings.Value;
            _context = context;
        }




      

        public async Task<CreatePaymentResult> CreatePaymentUrl(int purId)
        {

            PayOS payOS = new PayOS(_payOSSettings.ClientID, _payOSSettings.ApiKey, _payOSSettings.ChecksumKey);

            var purPk = await _context.PurchasePackages.Include(x => x.Package).Include(c => c.User).FirstOrDefaultAsync(c => c.PurchasePackageId == purId && c.Status == "Pending");

            if (purPk == null)
            {
                throw new ArgumentException(MessageConstants.ORDER_NOT_FOUND);
            }
            List<ItemData> items = new List<ItemData>();
           

                ItemData itemm = new ItemData(
                    name: "Booking Payment",
                    quantity: 1,
                    price: (int)purPk.Package.Price
                    );
                items.Add(itemm);
            
            Random random = new Random();
            var PaymentCode = $"POS{random.Next(1000000, 9999999)}";



            long expiredAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 600;

            var paymentData = new PaymentData(
            purPk.PurchasePackageId,
            (int)purPk.Package.Price,
            MessageConstants.PAYMENT_DESCRIPTION + purPk.Package.PackageName,
            items,
            "https://moca.mom:2030/api/PaymentPackage/cancel",
            "https://moca.mom:2030/api/PaymentPackage/success",
            null, null, null, null, null,
            expiredAt);
            var createPayment = await payOS.createPaymentLink(paymentData);
            await _context.SaveChangesAsync();

            return createPayment;
        }

      

        public WebhookData VerifyWebhook(WebhookType webhook)
        {
            PayOS payOS = new PayOS(_payOSSettings.ClientID, _payOSSettings.ApiKey, _payOSSettings.ChecksumKey);

            return payOS.verifyPaymentWebhookData(webhook);
        }
    }
}
