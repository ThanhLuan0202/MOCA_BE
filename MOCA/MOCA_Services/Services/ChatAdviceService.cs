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

        public async Task<string> GetAdviceAsync(string userId, int id)
        {
            if (!int.TryParse(userId, out int idUser))
            {
                throw new ArgumentException("Invalid user ID");
            }
            var momPr = await _momProfile.GetMomProfileByUserIdAsync(idUser);
            var userPregnancy = await _context.UserPregnancies.Include(up => up.Mom).Include(x => x.PregnancyTrackings)
                .FirstOrDefaultAsync(up => up.MomId == momPr.MomId && up.PregnancyId == id);


            if (userPregnancy == null) return "Không tìm thấy thông tin thai kỳ hiện tại.";

            var pregnancyInfo = userPregnancy.PregnancyTrackings.OrderByDescending(x => x.TrackingDate)
    .FirstOrDefault(c => c.PregnancyId == id);


            var trackings = await _context.BabyTrackings
                .OrderByDescending(bt => bt.CheckupDate)
                .FirstOrDefaultAsync(bt => bt.PregnancyId == id);

            var prompt = new StringBuilder();
            prompt.AppendLine("Tôi là một bác sĩ tư vấn thai kỳ.");
            prompt.AppendLine($"Tuần thai: {pregnancyInfo?.WeekNumber / 7}, Cân nặng của mẹ: {pregnancyInfo?.Weight}, Kích thước bụng: {pregnancyInfo?.BellySize}, Huyết áp: {pregnancyInfo?.BloodPressure}.");
            prompt.AppendLine("Các lần theo dõi thai gần đây:");


            prompt.AppendLine($"- Ngày: {trackings.CheckupDate:dd/MM/yyyy}, Nhịp tim thai: {trackings.FetalHeartRate}, Ước lượng cân nặng thai nhi : {trackings.EstimatedWeight}, Chỉ số nước ối : {trackings.AmnioticFluidIndex}, Vị trí nhau thai:{trackings.PlacentaPosition}, Ghi chú của bác sĩ sau khi khám:{trackings.DoctorComment}");


            prompt.AppendLine("Dựa vào các thông tin trên, hãy phân tích và đưa ra lời khuyên chi tiết nhất cho thai phụ, như ăn uống, nghỉ dưỡng , ngủ nghỉ, nên đi khám thai vào ngày nào theo bạn tính. Nếu nó bình thường thì vẫn phải đưa lời khuyên để thai phụ tiếp tục duy trì. Nếu không ổn hoặc không bình thường, phải nói cho rõ và phân tích nên làm gì làm gì .Nhớ là chi tiết và chính xác nhất nhé, phân tích từng chỉ số ví dụ như nhịp tim 5: là như nào như nào đó...., thông tin mà bạn được cung cấp, dễ hiểu, và một điều quan trọng là phải sử dụng thái độ và câu từ dịu dàng và thân thiện nhất như một người bạn thân để cho người dùng thoải mái không căng thẳng nhất. ");

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
