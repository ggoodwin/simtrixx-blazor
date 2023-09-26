using Application.Requests.Messaging;

namespace Application.Interfaces.Services.Messaging
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
        Task SendToMultipleAsync(MultipleMailRequest request);
    }
}
