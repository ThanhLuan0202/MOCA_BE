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
        private readonly IHttpClientFactory _httpClientFactory;


        public PayPalService(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _http = new HttpClient();
            _httpClientFactory = httpClientFactory;
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

        public async Task<(string orderId, string approvalUrl)> CreatePaymentWithOrderId(decimal amount, string returnUrl, string cancelUrl)
        {
            var accessToken = await GetAccessToken();

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var requestBody = new
            {
                intent = "CAPTURE",
                purchase_units = new[]
                {
                new
                {
                    amount = new { currency_code = "USD", value = amount.ToString("F2") }
                }
            },
                application_context = new
                {
                    return_url = returnUrl,
                    cancel_url = cancelUrl
                }
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://api-m.sandbox.paypal.com/v2/checkout/orders", jsonContent);
            var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

            var orderId = json.RootElement.GetProperty("id").GetString();
            var approvalUrl = json.RootElement.GetProperty("links")
                .EnumerateArray()
                .First(x => x.GetProperty("rel").GetString() == "approve")
                .GetProperty("href").GetString();

            return (orderId, approvalUrl);
        }

        private async Task<string> GetAccessToken()
        {
            var clientId = _config["PayPal:ClientId"];
            var secret = _config["PayPal:Secret"];
            var client = _httpClientFactory.CreateClient();

            var byteArray = Encoding.UTF8.GetBytes($"{clientId}:{secret}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var requestBody = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("grant_type", "client_credentials")
        });

            var response = await client.PostAsync("https://api-m.sandbox.paypal.com/v1/oauth2/token", requestBody);
            var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            return json.RootElement.GetProperty("access_token").GetString();
        }

    }
}
