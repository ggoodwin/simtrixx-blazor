using Application.Interfaces.Services.Messaging;
using Application.Requests.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers.Messaging
{
    public class EmailController :BaseApiController<EmailController>
    {
        private readonly IMailService _mailService;

        public EmailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost]
        public async Task SendEmailAsync(MailRequest request)
        {
            await _mailService.SendAsync(request);
        }

        [HttpPost]
        [Route("Multiple")]
        public async Task SendMultipleEmailsAsync(MultipleMailRequest request)
        {
            await _mailService.SendToMultipleAsync(request);
        }
    }
}
