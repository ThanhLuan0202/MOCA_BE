using MOCA_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MOCA_Services.Services
{
    public class PayPalService : IPayPalService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _http;

        public PayPalService(IConfiguration config)
        {
            _config = config;
            _http = new HttpClient();
        }

        public async Task<string> CreatePaymentUrl(decimal amount, string returnUrl, string cancelUrl)
        {
            var accessToken = await GetAccessToken();

            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var payload = new
            {
                intent = "CAPTURE",
                purchase_units = new[]
                {
                new {
                    amount = new {
                        currency_code = "USD",
                        value = amount.ToString("F2")
                    }
                }
            },
                application_context = new
                {
                    return_url = returnUrl,
                    cancel_url = cancelUrl
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var response = await _http.PostAsync($"{_config["PayPal:ApiUrl"]}/v2/checkout/orders", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            var json = JsonDocument.Parse(responseBody);
            var approvalUrl = json.RootElement
                .GetProperty("links")
                .EnumerateArray()
                .First(x => x.GetProperty("rel").GetString() == "approve")
                .GetProperty("href").GetString();

            return approvalUrl!;
        }

        private async Task<string> GetAccessToken()
        {
            var clientId = _config.GetValue<string>("PayPal:ClientId");
            var secret = _config.GetValue<string>("PayPal:Secret");
            var apiUrl = _config.GetValue<string>("PayPal:ApiUrl");

            var authValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{secret}"));

            var request = new HttpRequestMessage(HttpMethod.Post, $"{apiUrl}/v1/oauth2/token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authValue);
            request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await _http.SendAsync(request);
            var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

            return json.RootElement.GetProperty("access_token").GetString();
        }

    }
}
