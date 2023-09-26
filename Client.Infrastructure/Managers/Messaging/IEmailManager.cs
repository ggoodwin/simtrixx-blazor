using Application.Requests.Messaging;

namespace Client.Infrastructure.Managers.Messaging
{
    public interface IEmailManager : IManager
    {
        Task SendMailAsync(MailRequest request);
        Task SendMultipleMailAsync(MultipleMailRequest request);
    }
}
