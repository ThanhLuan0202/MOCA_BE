using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MOCA_Repositories.DBContext;
using MOCA_Services.Interfaces;
using PayPalCheckoutSdk.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.Services
{
    public class PrenatalReminderService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<PrenatalReminderService> _logger;

        public PrenatalReminderService(IServiceScopeFactory scopeFactory, ILogger<PrenatalReminderService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MOCAContext>();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                try
                {
                    var vnZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                    var vnNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vnZone);

                    
                    if (vnNow.Hour != 10 && vnNow.Hour != 20)
                    {
                        await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                        continue;
                    }

                    var targetDate = DateOnly.FromDateTime(vnNow.AddDays(1)); 

                    var checkups = await dbContext.BabyTrackings
                        .Include(x => x.Pregnancy)
                            .ThenInclude(p => p.Mom)
                                .ThenInclude(m => m.User)
                        .Where(x => x.CheckupDate.HasValue && x.CheckupDate.Value == targetDate)
                        .ToListAsync(stoppingToken);

                    foreach (var checkup in checkups)
                    {
                        var user = checkup.Pregnancy?.Mom?.User;
                        _logger.LogInformation($"CheckupID: {checkup.CheckupBabyId}, Email: {user?.Email}");

                        if (user != null && !string.IsNullOrEmpty(user.Email))
                        {
                            var subject = "Nhắc nhở khám thai ngày mai";
                            var body = $"Chào {user.FullName}, bạn có lịch khám thai vào ngày mai ({checkup.CheckupDate?.ToString("dd/MM/yyyy")}). Vui lòng đến đúng giờ nhé!";
                            _logger.LogInformation($"Gửi email tới: {user.Email}");
                            await emailService.SendEmailAsync(user.Email, subject, body);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Lỗi khi gửi email nhắc nhở mẹ bầu");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);

            }
        }

    }

}
