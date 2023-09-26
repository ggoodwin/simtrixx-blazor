using Application.Configurations;
using Application.Interfaces.Services.Messaging;
using Application.Requests.Messaging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

namespace Infrastructure.Services.Messaging
{
    public class TwilioService : ITwilioService
    {
        private readonly TwilioConfiguration _config;
        private readonly ILogger<TwilioService> _logger;

        public TwilioService(IOptions<TwilioConfiguration> config, ILogger<TwilioService> logger)
        {
            _config = config.Value;
            _logger = logger;
        }

        public async Task<string> SendAsync(TwilioRequest request)
        {
            TwilioClient.Init(_config.AccountId, _config.AuthToken);

            var message = await MessageResource.CreateAsync(
                new PhoneNumber(request.PhoneNumber),
                @from: new PhoneNumber(_config.SendNumber),
                body: request.Message
            );
            return message.Sid;
        }
    }
}
