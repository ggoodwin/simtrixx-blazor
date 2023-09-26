using Application.Requests.Identity;
using Blazored.FluentValidation;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Authentication
{
    public partial class Register
    {
        [Inject] private ILocalStorageService LocalStorage { get; set; }

        private FluentValidationValidator? _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator != null && _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private RegisterRequest _registerUserModel = new();
        private bool _disabled;
        private bool _showError;
        private string _errorMessage;

        protected override async Task OnInitializedAsync()
        {
            _registerUserModel.FirstName = await LocalStorage.GetItemAsync<string>("firstname");
            _registerUserModel.LastName = await LocalStorage.GetItemAsync<string>("lastname");
            _registerUserModel.Email = await LocalStorage.GetItemAsync<string>("email");
        }

        private async Task SubmitAsync()
        {
            _disabled = true;
            var response = await _userManager.RegisterUserAsync(_registerUserModel);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                _navigationManager.NavigateTo("/login");
                _registerUserModel = new RegisterRequest();
            }
            else
            {
                _disabled = false;
                _showError = true;
                foreach (var message in response.Messages)
                {
                    _errorMessage = message;
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private bool _passwordVisibility;
        private InputType _passwordInput = InputType.Password;
        private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

        private void TogglePasswordVisibility()
        {
            if (_passwordVisibility)
            {
                _passwordVisibility = false;
                _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
                _passwordInput = InputType.Password;
            }
            else
            {
                _passwordVisibility = true;
                _passwordInputIcon = Icons.Material.Filled.Visibility;
                _passwordInput = InputType.Text;
            }
        }
    }
}
