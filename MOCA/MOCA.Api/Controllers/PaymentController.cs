using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MOCA_Repositories.DBContext;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly MOCAContext _context;
    private readonly IConfiguration _config;

    public PaymentController(IHttpClientFactory httpClientFactory, MOCAContext context, IConfiguration config)
    {
        _httpClientFactory = httpClientFactory;
        _context = context;
        _config = config;
    }

    [HttpGet("paypal-return")]
    public async Task<IActionResult> PayPalReturn([FromQuery] string token, [FromQuery] string paymentId)
    {
        var accessToken = await GetAccessToken();
        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        // 🟡 1. Gọi capture đơn hàng PayPal
        var request = new HttpRequestMessage(HttpMethod.Post, $"https://api-m.sandbox.paypal.com/v2/checkout/orders/{token}/capture");
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        request.Content = new StringContent("{}", Encoding.UTF8, "application/json");

        var captureResponse = await client.SendAsync(request);

        var captureJson = await captureResponse.Content.ReadAsStringAsync();

        JsonDocument result;
        try
        {
            result = JsonDocument.Parse(captureJson);
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                message = "Lỗi khi đọc phản hồi capture từ PayPal.",
                error = ex.Message,
                raw = captureJson
            });
        }

        if (!result.RootElement.TryGetProperty("status", out var statusProp))
        {
            return BadRequest(new
            {
                message = "Không có thuộc tính 'status' trong JSON trả về.",
                raw = captureJson
            });
        }

        var status = statusProp.GetString();
        if (status != "COMPLETED")
        {
            return BadRequest(new
            {
                message = "Thanh toán chưa hoàn tất",
                status,
                raw = captureJson
            });
        }

        // 🟢 2. Cập nhật thanh toán trong hệ thống của bạn
        var payment = await _context.BookingPayments
            .Include(x => x.Booking)
            .FirstOrDefaultAsync(x => x.PaymentId == int.Parse(paymentId));

        if (payment == null)
        {
            return NotFound(new
            {
                message = "Không tìm thấy thông tin thanh toán",
                paymentId
            });
        }

        payment.IsPaid = true;
        payment.PaymentDate = DateTime.Now;
        var booking = await _context.DoctorBookings.FirstOrDefaultAsync(b => b.BookingId == payment.BookingId);
        if (booking != null)
        {
            booking.Status = "Confirm";
            _context.Entry(booking).State = EntityState.Modified;

        }
        _context.Entry(payment).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        // 🟢 3. Trả kết quả
        return Ok(new
        {
            message = "Thanh toán thành công",
            paymentId = payment.PaymentId,
            bookingId = payment.BookingId,
            amount = payment.Amount,
            service = payment.Booking.ConsultationType.ToString(),
            bookingTime = payment.Booking.BookingDate
        });
    }



    [HttpGet("paypal-cancel")]
    public async Task<IActionResult> PayPalCancel(string paymentId)
    {
        if (!int.TryParse(paymentId, out int pid))
        {
            return BadRequest("Invalid payment ID.");
        }

        var payment = await _context.BookingPayments.FindAsync(pid);
        if (payment == null)
        {
            return NotFound("Payment not found.");
        }

        // Tìm booking tương ứng
        var booking = await _context.DoctorBookings.FindAsync(payment.BookingId);
        if (booking != null)
        {
            booking.Status = "Inactive";
            await _context.SaveChangesAsync();
        }

        // Có thể redirect về trang thông báo "Đã huỷ thanh toán"
        return Redirect("https://localhost:5173/payment-cancel"); // tùy frontend
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
