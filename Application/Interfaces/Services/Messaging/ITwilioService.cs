using Application.Requests.Messaging;

namespace Application.Interfaces.Services.Messaging
{
    public interface ITwilioService
    {
        Task<string> SendAsync(TwilioRequest request);
    }
}
