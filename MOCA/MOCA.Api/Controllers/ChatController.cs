using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "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";

        public ChatController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public class ChatRequest
        {
            public string Message { get; set; }
        }

        public class ChatResponse
        {
            public string Reply { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ChatRequest request)
        {
            var messages = new[]
            {
            new { role = "system", content = "Bạn là một bác sĩ ảo chuyên tư vấn sức khỏe cho mẹ bầu, hãy trả lời bằng tiếng Việt, ngắn gọn và nhẹ nhàng." },
            new { role = "user", content = request.Message }
        };

            var payload = new
            {
                model = "gpt-3.5-turbo", 
                messages = messages,
                temperature = 0.7
            };

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
            httpRequest.Headers.Add("Authorization", $"Bearer {_apiKey}");
            httpRequest.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(httpRequest);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, json);

            using var doc = JsonDocument.Parse(json);
            var reply = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return Ok(new ChatResponse { Reply = reply });
        }
    }
}
