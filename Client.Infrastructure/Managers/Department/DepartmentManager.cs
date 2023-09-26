using System.Net.Http.Json;
using Application.Configurations;
using Application.Features.Departments.Commands.AddEdit;
using Application.Features.Departments.Queries.GetAll;
using Client.Infrastructure.Extensions;
using Common.Wrapper;
using Microsoft.Extensions.Options;

namespace Client.Infrastructure.Managers.Department
{
    public class DepartmentManager : IDepartmentManager
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<UrlConfiguration> _config;

        public DepartmentManager(HttpClient httpClient, IOptions<UrlConfiguration> config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<IResult<IEnumerable<GetAllDepartmentsResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{_config.Value.Api}/{Routes.DepartmentEndpoints.GetAll}");
            return await response.ToResult<IEnumerable<GetAllDepartmentsResponse>>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditDepartmentCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_config.Value.Api}/{Routes.DepartmentEndpoints.Save}", request);
            return await response.ToResult<int>();
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_config.Value.Api}/{Routes.DepartmentEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }
    }
}
