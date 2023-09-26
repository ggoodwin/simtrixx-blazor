using Application.Features.Licenses.Commands.AddEdit;
using Application.Responses.Identity;
using Blazored.FluentValidation;
using Client.Infrastructure.Managers.License;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace Client.Pages.Admin.Licenses
{
    public partial class AddEditLicenseModal
    {
        [Inject] private ILicenseManager? LicenseManager { get; set; }

        [Parameter] public AddEditLicenseCommand AddEditLicenseModel { get; set; } = new();
        [CascadingParameter] private MudDialogInstance? MudDialog { get; set; }

        private FluentValidationValidator? _fluentValidationValidator;
        private bool _disabled;
        private List<UserResponse> _users = new();

        public void Cancel()
        {
            MudDialog?.Cancel();
        }

        private async Task SaveAsync()
        {
            _disabled = true;
            var response = await LicenseManager.SaveAsync(AddEditLicenseModel);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                MudDialog?.Close();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private async Task LoadUsersAsync()
        {
            var data = await _userManager.GetAllAsync();
            if (data.Succeeded)
            {
                _users = data.Data;
            }
            AddEditLicenseModel.Expiration = DateTime.Now.AddYears(1);
            AddEditLicenseModel.Key = Guid.NewGuid().ToString();
            await Task.CompletedTask;
        }

        private async Task<IEnumerable<string?>> SearchUsers(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return _users.Select(x => x.Id);

            return _users.Where(x => x.UserName.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Id);
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadUsersAsync();
        }

        private async Task CopyToClipboard()
        {
            await _jsRuntime.InvokeVoidAsync("clipboardCopy.copyText", AddEditLicenseModel.Key);
            _snackBar.Add("Key Copied to Clipboard", Severity.Success);
        }
    }
}
