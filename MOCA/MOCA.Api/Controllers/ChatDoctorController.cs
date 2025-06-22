using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Services.Interfaces;
using System;
using System.Security.Claims;

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatDoctorController : ControllerBase
    {
        private readonly MOCAContext _context;
        private readonly IDoctorProfileService _doctor;


        public ChatDoctorController(MOCAContext context, IDoctorProfileService doctor)
        {
            _context = context;
            _doctor = doctor;
        }

        // danh sách cuộc trò chuyện 
        [HttpGet("contacts")]
        public async Task<IActionResult> GetContacts()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (!int.TryParse(userId, out int idUser))
            {
                return BadRequest("Invalid user ID format.");
            }
            var role = User.FindFirst(ClaimTypes.Role).Value;
            
            IQueryable<DoctorContact> query = _context.DoctorContacts;

            if (role == "Doctor") {
                var doctorCheck = await _doctor.GetDoctorProfileByUserIdAsync(userId);
                query = query.Where(c => c.DoctorId == doctorCheck.DoctorId);

            }
            else
                query = query.Where(c => c.UserId == idUser);

            var contacts = await query.ToListAsync();
            return Ok(contacts);
        }

        //  lịch sử 
        [HttpGet("messages/{contactId}")]
        public async Task<IActionResult> GetMessages(int contactId)
        {
            var messages = await _context.MessagesWithDoctors
                .Where(m => m.ContactId == contactId)
                .OrderBy(m => m.SendAt)
                .ToListAsync();
            return Ok(messages);
        }

        // Gửi tn
        [HttpPost("messages")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var role = User.FindFirst(ClaimTypes.Role).Value;

            var msg = new MessagesWithDoctor
            {
                ContactId = dto.ContactId,
                SenderType = role,
                MessageText = dto.MessageText,
                SendAt = DateTime.UtcNow
            };
            _context.MessagesWithDoctors.Add(msg);
            await _context.SaveChangesAsync();
            return Ok(msg);
        }
        public class SendMessageDto
        {
            public int ContactId { get; set; }
            public string MessageText { get; set; }
        }

    }
}