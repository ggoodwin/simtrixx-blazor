using Application.Requests.Identity;
using Client.Extensions;
using Client.Infrastructure.Settings;
using MudBlazor;

namespace Client.Shared.Layout
{
    public partial class HomeLayout : IDisposable
    {
        private RegisterLightRequest _registerUserModel = new();
        private MudTheme? _currentTheme;
        private string? UserName { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadUser();
            _currentTheme = SimtrixxTheme.DefaultTheme;
            _currentTheme = await _clientPreferenceManager.GetCurrentThemeAsync();
            _interceptor.RegisterEvent();
        }

        private async Task LoadUser()
        {
            //Get UserName
            var state = await _stateProvider.GetAuthenticationStateAsync();
            var user = state.User;
            if(user.Identity is { IsAuthenticated: true }) UserName = $"{user.GetFirstName()} {user.GetLastName()}";
        }

        private async Task DarkMode()
        {
            bool isDarkMode = await _clientPreferenceManager.ToggleDarkModeAsync();
            _currentTheme = isDarkMode
                ? SimtrixxTheme.DefaultTheme
                : SimtrixxTheme.DarkTheme;
        }

        private async Task SubmitAsync()
        {
            await _userManager.RegisterUserLightAsync(_registerUserModel);
            _snackBar.Add("Redirecting to Register", Severity.Success);
            _navigationManager.NavigateTo("/register");
            _registerUserModel = new RegisterLightRequest();
        }

        public void Dispose()
        {
            _interceptor.DisposeEvent();
        }
    }
}
