using Application.Configurations;
using Application.Interfaces.Services.Messaging;
using Application.Requests.Messaging;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.Services.Messaging
{
    public class SmtpService : IMailService
    {
        private readonly MailConfiguration _config;
        private readonly ILogger<SmtpService> _logger;

        public SmtpService(IOptions<MailConfiguration> config, ILogger<SmtpService> logger)
        {
            _config = config.Value;
            _logger = logger;
        }

        public async Task SendToMultipleAsync(MultipleMailRequest request)
        {
        }

        public async Task SendAsync(MailRequest request)
        {
            try
            {
                var email = new MimeMessage
                {
                    Sender = new MailboxAddress(_config.DisplayName, _config.From),
                    Subject = request.Subject,
                    Body = new BodyBuilder
                    {
                        HtmlBody = request.Body
                    }.ToMessageBody()
                };
                email.To.Add(MailboxAddress.Parse(request.To));
                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_config.Host, _config.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_config.UserName, _config.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }
}
