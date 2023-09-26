using System.Net.Http.Json;
using Application.Configurations;
using Application.Features.Licenses.Commands.AddEdit;
using Application.Features.Licenses.Commands.Import;
using Application.Features.Licenses.Queries.GetAll;
using Application.Features.Licenses.Queries.GetById;
using Application.Features.Licenses.Queries.GetByUser;
using Client.Infrastructure.Extensions;
using Common.Wrapper;
using Microsoft.Extensions.Options;

namespace Client.Infrastructure.Managers.License
{
    public class LicenseManager : ILicenseManager
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<UrlConfiguration> _config;

        public LicenseManager(HttpClient httpClient, IOptions<UrlConfiguration> config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<IResult<IEnumerable<GetAllLicensesResponse>>> GetAllLicensesAsync()
        {
            var response = await _httpClient.GetAsync($"{_config.Value.Api}/{Routes.LicenseEndpoints.GetAllLicenses}");
            return await response.ToResult<IEnumerable<GetAllLicensesResponse>>();
        }

        public async Task<IResult<IEnumerable<GetLicensesByUserResponse>>> GetLicensesByUserAsync()
        {
            var response = await _httpClient.GetAsync($"{_config.Value.Api}/{Routes.LicenseEndpoints.GetLicenseByUser}");
            return await response.ToResult<IEnumerable<GetLicensesByUserResponse>>();
        }

        public async Task<IResult<GetLicenseByIdResponse>> GetLicenseByIdAsync(GetLicenseByIdQuery request)
        {
            var response = await _httpClient.GetAsync($"{_config.Value.Api}/{Routes.LicenseEndpoints.GetLicense(request.Id)}");
            return await response.ToResult<GetLicenseByIdResponse>();
        }

        public async Task<IResult<string>> DownloadFileAsync(string searchString)
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? $"{_config.Value.Api}/{Routes.LicenseEndpoints.DownloadFile}"
                : _config.Value.Api + "/" + Routes.LicenseEndpoints.DownloadFileFiltered(searchString));
            return await response.ToResult<string>();
        }

        public async Task<IResult<int>> ImportAsync(ImportLicensesCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_config.Value.Api}/{Routes.LicenseEndpoints.Import}", request);
            return await response.ToResult<int>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditLicenseCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_config.Value.Api}/{Routes.LicenseEndpoints.Save}", request);
            return await response.ToResult<int>();
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_config.Value.Api}/{Routes.LicenseEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }
    }
}
