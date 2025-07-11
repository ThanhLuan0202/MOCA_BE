using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.PayOS;
using MOCA_Services.Constants;
using MOCA_Services.Interfaces;
using Net.payOS;
using Net.payOS.Types;
using PayPalCheckoutSdk.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.Services
{
    public class PayOSService : IPayOSService
    {
        private readonly PayOSSettings _payOSSettings;
        private readonly MOCAContext _context;

        public PayOSService(IOptions<PayOSSettings> payOSSettings, MOCAContext context)
        {
            _payOSSettings = payOSSettings.Value;
            _context = context;
        }




        public  Task<PaymentLinkInformation> CancelOrder(string orderId, string reason)
        {
            throw new NotImplementedException();

        }

        public async Task<CreatePaymentResult> CreatePaymentUrl(int orderId)
        {
            
            PayOS payOS = new PayOS(_payOSSettings.ClientID, _payOSSettings.ApiKey, _payOSSettings.ChecksumKey);

            var bookingPayment = await _context.DoctorBookings.Include(x => x.BookingPayments).FirstOrDefaultAsync(c => c.BookingId == orderId && c.Status == "Pending");

            if (bookingPayment == null)
            {
                throw new ArgumentException(MessageConstants.ORDER_NOT_FOUND);
            }
            List<ItemData> items = new List<ItemData>();
            foreach (var item in bookingPayment.BookingPayments)
            {
                if (bookingPayment.BookingPayments == null)
                {
                    throw new ArgumentException("Product not found in order detail.");
                }

                ItemData itemm = new ItemData(
                    name: "Booking Payment",
                    quantity: 1,
                    price: (int)item.Amount
                    );
                items.Add(itemm);
            }
            Random random = new Random();
            var PaymentCode = $"POS{random.Next(1000000, 9999999)}";

            var payment = new BookingPayment
            {
                BookingId = orderId,
                Amount = bookingPayment.Price,
                PaymentMethod = "PayOS",
                IsPaid = false,
                PaymentDate = DateTime.UtcNow,
                PaypalOrderId = null
            };

            await _context.BookingPayments.AddAsync(payment);
            await _context.SaveChangesAsync();





            long expiredAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 600;

            var paymentData = new PaymentData(
            bookingPayment.BookingId,
            (int)payment.Amount,
            MessageConstants.PAYMENT_DESCRIPTION + bookingPayment.Notes,
            items,
            _payOSSettings.CancelUrl,
            _payOSSettings.ReturnUrl,
            null, null, null, null, null,
            expiredAt);
            var createPayment = await payOS.createPaymentLink(paymentData);
            payment.PaypalOrderId = createPayment.paymentLinkId.ToString();
            await _context.SaveChangesAsync();

            return createPayment;
        }

        public Task<PaymentLinkInformation> GetPaymentInfo(int orderId)
        {
            throw new NotImplementedException();
        }

        public WebhookData VerifyWebhook(WebhookType webhook)
        {
            PayOS payOS = new PayOS(_payOSSettings.ClientID, _payOSSettings.ApiKey, _payOSSettings.ChecksumKey);

            return payOS.verifyPaymentWebhookData(webhook);
        }
    }
}
