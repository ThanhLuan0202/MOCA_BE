using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.Services
{
    // Services/ChatService.cs
    public class ChatService
    {
        private readonly MOCAContext _context;

        public ChatService(MOCAContext context) { _context = context; }

        public async Task<MessagesWithDoctor> SaveMessageAsync(int contactId, string senderType, string message)
        {
            var msg = new MessagesWithDoctor
            {
                ContactId = contactId,
                SenderType = senderType,
                MessageText = message,
                SendAt = DateTime.UtcNow
            };
            _context.MessagesWithDoctors.Add(msg);
            await _context.SaveChangesAsync();
            return msg;
        }
    }
}
