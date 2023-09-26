using System.Net.Http.Json;
using Application.Configurations;
using Application.Features.ContactRequests.Commands.AddEdit;
using Application.Features.ContactRequests.Queries.GetAll;
using Client.Infrastructure.Extensions;
using Common.Wrapper;
using Microsoft.Extensions.Options;

namespace Client.Infrastructure.Managers.ContactRequest
{
    public class ContactRequestManager : IContactRequestManager
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<UrlConfiguration> _config;

        public ContactRequestManager(HttpClient httpClient, IOptions<UrlConfiguration> config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<IResult<IEnumerable<GetAllContactRequestsResponse>>> GetAllContactRequestsAsync()
        {
            var response = await _httpClient.GetAsync($"{_config.Value.Api}/{Routes.ContactRequestEndpoints.GetAll}");
            return await response.ToResult<IEnumerable<GetAllContactRequestsResponse>>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditContactRequestCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_config.Value.Api}/{Routes.ContactRequestEndpoints.Save}", request);
            return await response.ToResult<int>();
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_config.Value.Api}/{Routes.ContactRequestEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }
    }
}
