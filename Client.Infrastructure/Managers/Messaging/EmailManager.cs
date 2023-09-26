using System.Net.Http.Json;
using Application.Configurations;
using Application.Requests.Messaging;
using Microsoft.Extensions.Options;

namespace Client.Infrastructure.Managers.Messaging
{
    public class EmailManager : IEmailManager
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<UrlConfiguration> _config;

        public EmailManager(HttpClient httpClient, IOptions<UrlConfiguration> config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task SendMailAsync(MailRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_config.Value.Api}/{Routes.MessagingEndpoints.Send}", request);
            //return await response.ToResult<int>();
        }

        public async Task SendMultipleMailAsync(MultipleMailRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_config.Value.Api}/{Routes.MessagingEndpoints.SendMultiple}", request);
            //return await response.ToResult<int>();
        }
    }
}
