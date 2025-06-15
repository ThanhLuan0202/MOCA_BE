using AutoMapper.Configuration;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MOCA_Repositories.Models.ModelMail;
using MOCA_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
namespace MOCA_Services.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;

        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.From);
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;

            var builder = new BodyBuilder
            {
                HtmlBody = body
            };
            email.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            await smtp.ConnectAsync(_mailSettings.SmtpServer, _mailSettings.Port, true);
            await smtp.AuthenticateAsync(_mailSettings.From, _mailSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
        public async Task SendVerificationEmailToDoctorAsync(string toEmail, string fullName)
        {
            string subject = "Xác minh thông tin bác sĩ - Hệ thống MOCA";

            string body = $@"
        <p>Xin chào bác sĩ <strong>{fullName}</strong>,</p>

        <p>Chúng tôi đã nhận được yêu cầu đăng ký tài khoản bác sĩ của bạn trên hệ thống <strong>MOCA</strong>.</p>

        <p>Để xác minh danh tính và đảm bảo chất lượng dịch vụ, vui lòng gửi lại các thông tin sau:</p>

        <ul>
            <li>Ảnh <strong>Chứng chỉ hành nghề (CCHN)</strong></li>
            <li>Ảnh <strong>CMND/CCCD</strong> (mặt trước)</li>
            <li>Thông tin nơi đang công tác (Bệnh viện / Phòng khám)</li>
        </ul>

        <p>Vui lòng trả lời email này kèm theo các file đính kèm để hoàn tất xác minh.</p>
        <p>Chúng tôi xin cam kết mọi thông tin được cung cấp sẽ được bảo mật.</p>

        <p>Chúng tôi sẽ phản hồi trong vòng 24h. Xin cảm ơn!</p>

        <br/>
        <p><em>Đội ngũ MOCA</em></p>
    ";

            await SendEmailAsync(toEmail, subject, body);

        }

    }
}
