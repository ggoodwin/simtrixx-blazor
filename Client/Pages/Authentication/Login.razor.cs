using Application.Requests.Identity;
using Blazored.FluentValidation;
using MudBlazor;

namespace Client.Pages.Authentication
{
    public partial class Login
    {
        private FluentValidationValidator? _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator != null && _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private TokenRequest _tokenModel = new();
        private bool _disabled;

        protected override async Task OnInitializedAsync()
        {
            var state = await _stateProvider.GetAuthenticationStateAsync();
            if (state.User.Identity is { IsAuthenticated: true })
            {
                _navigationManager.NavigateTo("/account");
            }
        }

        private void GoogleLogin()
        {
            _disabled = true;
            _navigationManager.NavigateTo("/authentication/login");
        }

        private async Task SubmitAsync()
        {
            _disabled = true;
            var result = await _authenticationManager.Login(_tokenModel);
            if (!result.Succeeded)
            {
                foreach (var message in result.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
            else
            {
                _navigationManager.NavigateTo("/account", forceLoad:true);
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
