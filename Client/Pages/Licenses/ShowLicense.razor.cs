using Application.Features.Licenses.Queries.GetByUser;
using Client.Infrastructure.Managers.License;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Licenses
{
    [Authorize]
    public partial class ShowLicense
    {
        private bool _loaded;
        [Inject] private ILicenseManager? LicenseManager { get; set; }
        private GetLicensesByUserResponse _license = new();
        private List<GetLicensesByUserResponse> _licenses = new();
        private string _searchString = "";

        protected override async Task OnInitializedAsync()
        {
            await GetDataAsync();
            _loaded = true;
        }

        private async Task GetDataAsync()
        {
            var response = await LicenseManager?.GetLicensesByUserAsync()!;
            if (response.Succeeded)
            {
                _licenses = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private bool Search(GetLicensesByUserResponse response)
        {
            var result = string.IsNullOrWhiteSpace(_searchString);
            
            if (result) return result;
            if (response.Key.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            {
                result = true;
            }
            return result;
        }
    }
}
