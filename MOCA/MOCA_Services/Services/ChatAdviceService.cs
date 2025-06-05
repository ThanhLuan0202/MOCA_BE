using Firebase.Storage;
using MOCA_Repositories.DBContext;
using MOCA_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using MOCA_Repositories.Interfaces;

namespace MOCA_Services.Services
{
    public class ChatAdviceService : IChatAdviceService
    {
        private readonly MOCAContext _context;
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly IMomProfileRepository _momProfile;

        public ChatAdviceService(MOCAContext context, IHttpClientFactory factory, IConfiguration configuration, IMomProfileRepository momProfile)
        {
            _context = context;
            _httpClient = factory.CreateClient();
            _momProfile = momProfile;
            _apiKey = configuration["OpenAI:ApiKey"];
        }

        public async Task<string> GetAdviceAsync(string userId)
        {
            if (!int.TryParse(userId, out int idUser))
            {
                throw new ArgumentException("Invalid user ID");
            }
            var momPr = await _momProfile.GetMomProfileByUserIdAsync(idUser);
            var userPregnancy = await _context.UserPregnancies.Include(up => up.Mom).Include(x => x.PregnancyTrackings)
                .FirstOrDefaultAsync(up => up.MomId == momPr.MomId);
            

            if (userPregnancy == null) return "Không tìm thấy thông tin thai kỳ hiện tại.";

            var pregnancyInfo = userPregnancy.PregnancyTrackings.OrderByDescending(x => x.TrackingDate)
    .FirstOrDefault();


            var trackings = await _context.BabyTrackings
                .Where(bt => bt.PregnancyId == userPregnancy.PregnancyId)
                .OrderByDescending(bt => bt.CheckupDate)
                .Take(5) 
                .ToListAsync();

            var prompt = new StringBuilder();
            prompt.AppendLine("Tôi là một bác sĩ tư vấn thai kỳ.");
            prompt.AppendLine($"Tuần thai: {pregnancyInfo?.WeekNumber/7}, Cân nặng của mẹ: {pregnancyInfo?.Weight}, Kích thước bụng: {pregnancyInfo?.BellySize}, Huyết áp: {pregnancyInfo?.BloodPressure}.");
            prompt.AppendLine("Các lần theo dõi thai gần đây:");

            foreach (var t in trackings)
            {
                prompt.AppendLine($"- Ngày: {t.CheckupDate:dd/MM/yyyy}, Nhịp tim thai: {t.FetalHeartRate}, Ước lượng cân nặng thai nhi : {t.EstimatedWeight}, Chỉ số nước ối : {t.AmnioticFluidIndex}, Vị trí nhau thai:{t.PlacentaPosition}, Ghi chú của bác sĩ sau khi khám:{t.DoctorComment}");
            }

            prompt.AppendLine("Dựa vào các thông tin trên, hãy phân tích và đưa ra lời khuyên chi tiết cho thai phụ.");

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                new { role = "system", content = "Bạn là bác sĩ tư vấn thai kỳ." },
                new { role = "user", content = prompt.ToString() }
            }
            };

            var requestJson = JsonSerializer.Serialize(requestBody);

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
            request.Headers.Add("Authorization", $"Bearer {_apiKey}");
            request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode) return "Lỗi khi gọi ChatGPT.";

            var resultString = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(resultString);
            var message = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return message ?? "Không nhận được phản hồi từ ChatGPT.";
        }
    }

}
