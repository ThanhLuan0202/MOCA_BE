using Microsoft.AspNetCore.SignalR;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace MOCA_Services.Services
{
    public class ChatHub : Hub
    {
        private readonly MOCAContext _context;
        public ChatHub(MOCAContext context) { _context = context; }

        // Khi user/doctor join vào 1 contact (cuộc trò chuyện)
        public async Task JoinContact(int contactId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, contactId.ToString());
        }

        // Khi user/doctor rời khỏi contact
        public async Task LeaveContact(int contactId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, contactId.ToString());
        }

        // Gửi tin nhắn
        public async Task SendMessage(int contactId, string message)
        {
            // Lấy thông tin user từ token
            var userId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = Context.User.FindFirst(ClaimTypes.Role)?.Value;

            // Lưu vào DB
            var msg = new MessagesWithDoctor
            {
                ContactId = contactId,
                SenderType = role, // "Doctor" hoặc "User"
                MessageText = message,
                SendAt = DateTime.UtcNow
            };
            _context.MessagesWithDoctors.Add(msg);
            await _context.SaveChangesAsync();

            // Đẩy realtime cho tất cả client trong group contactId
            await Clients.Group(contactId.ToString()).SendAsync("ReceiveMessage", new
            {
                msg.MessageId,
                msg.ContactId,
                msg.SenderType,
                msg.MessageText,
                msg.SendAt
            });
        }
    }
}
