using Application.Configurations;
using Application.Interfaces.Services.Messaging;
using Application.Requests.Messaging;
using Azure.Core;
using Common.Wrapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Infrastructure.Services.Messaging
{
    public class SendGridService : IMailService
    {
        private readonly SendGridConfiguration _config;
        private readonly ILogger<SendGridService> _logger;

        public SendGridService(IOptions<SendGridConfiguration> config, ILogger<SendGridService> logger)
        {
            _config = config.Value;
            _logger = logger;
        }

        public async Task SendAsync(MailRequest request)
        {
            try
            {
                var client = new SendGridClient(_config.Key);
                var from = new EmailAddress(_config.From, _config.DisplayName);
                var subject = request.Subject;
                var to = new EmailAddress(request.To, request.ToName ?? request.To);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, request.Body, request.HtmlBody);
                var response = await client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }

        public async Task SendToMultipleAsync(MultipleMailRequest request)
        {
            try
            {
                var client = new SendGridClient(_config.Key);
                var from = new EmailAddress(_config.From, _config.DisplayName);
                var subject = request.Subject;
                var listOfEmails = new List<EmailAddress>();
                foreach (var email in request.To)
                {
                    var newEmail = new EmailAddress(email, email);
                    listOfEmails.Add(newEmail);
                }
                var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, listOfEmails, subject, request.Body,
                    request.HtmlBody);
                var response = await client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }
}
