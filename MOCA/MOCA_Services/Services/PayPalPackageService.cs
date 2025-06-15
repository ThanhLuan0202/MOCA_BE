using MOCA_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.Services
{
    using Microsoft.Extensions.Configuration;
    using PayPalCheckoutSdk.Core;
    using PayPalCheckoutSdk.Orders;

    public class PayPalPackageService : IPayPalPackageService
    {
        private readonly PayPalHttpClient _client;

        public PayPalPackageService(IConfiguration config)
        {
            var environment = new SandboxEnvironment(
                config["PayPal:ClientId"],
                config["PayPal:Secret"]
            );
            _client = new PayPalHttpClient(environment);
        }

        public async Task<string> CreatePaymentUrl(decimal amount, string returnUrl)
        {
            var orderRequest = new OrderRequest()
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>
            {
                new PurchaseUnitRequest
                {
                    AmountWithBreakdown = new AmountWithBreakdown
                    {
                        CurrencyCode = "USD",
                        Value = amount.ToString("F2")
                    }
                }
            },
                ApplicationContext = new ApplicationContext
                {
                    ReturnUrl = returnUrl,
                    CancelUrl = returnUrl,
                    BrandName = "MOCA",
                    UserAction = "PAY_NOW"
                }
            };

            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");
            request.RequestBody(orderRequest);

            var response = await _client.Execute(request);
            var result = response.Result<Order>();
            return result.Links.First(x => x.Rel == "approve").Href;
        }

        public async Task<bool> CapturePaymentAsync(string token)
        {
            var request = new OrdersCaptureRequest(token);
            request.RequestBody(new OrderActionRequest());

            var response = await _client.Execute(request);
            return response.StatusCode == System.Net.HttpStatusCode.Created;
        }
    }

}
