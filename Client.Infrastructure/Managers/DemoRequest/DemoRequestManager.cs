using System.Net.Http.Json;
using Application.Configurations;
using Application.Features.DemoRequests.Commands.AddEdit;
using Application.Features.DemoRequests.Queries.GetAll;
using Client.Infrastructure.Extensions;
using Common.Wrapper;
using Microsoft.Extensions.Options;

namespace Client.Infrastructure.Managers.DemoRequest
{
    public class DemoRequestManager : IDemoRequestManager
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<UrlConfiguration> _config;

        public DemoRequestManager(HttpClient httpClient, IOptions<UrlConfiguration> config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<IResult<IEnumerable<GetAllDemoRequestsResponse>>> GetAllDemoRequestsAsync()
        {
            var response = await _httpClient.GetAsync($"{_config.Value.Api}/{Routes.DemoRequestEndpoints.GetAllDemoRequests}");
            return await response.ToResult<IEnumerable<GetAllDemoRequestsResponse>>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditDemoRequestCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_config.Value.Api}/{Routes.DemoRequestEndpoints.Save}", request);
            return await response.ToResult<int>();
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_config.Value.Api}/{Routes.DemoRequestEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }
    }
}
