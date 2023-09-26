using Client.Infrastructure.Routes;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Identity
{
    public partial class Verify
    {
        [Parameter] public string? UserId { get; set; }
        [Parameter] public string? Code { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var apiUrl = $"{_config["Urls:Api"]}/{AccountEndpoints.Verify}/?userId={UserId}&code={Code}";
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                _snackBar.Add("Email Address Verified", Severity.Success);
            }
            else
            {
                _snackBar.Add("Verification Failed", Severity.Error);
            }
        }
    }
}
