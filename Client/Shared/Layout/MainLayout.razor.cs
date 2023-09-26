using Client.Infrastructure.Settings;
using MudBlazor;

namespace Client.Shared.Layout
{
    public partial class MainLayout : IDisposable
    {
        private MudTheme? _currentTheme;
        //private bool _isHome;

        protected override async Task OnInitializedAsync()
        {
            //if (_navigationManager.Uri == _config["Urls:Base"] || _navigationManager.Uri == _config["Urls:BaseWWW"]) _isHome = true;
            _currentTheme = SimtrixxTheme.DefaultTheme;
            _currentTheme = await _clientPreferenceManager.GetCurrentThemeAsync();
            _interceptor.RegisterEvent();
        }

        private async Task DarkMode()
        {
            bool isDarkMode = await _clientPreferenceManager.ToggleDarkModeAsync();
            _currentTheme = isDarkMode
                ? SimtrixxTheme.DefaultTheme
                : SimtrixxTheme.DarkTheme;
        }

        public void Dispose()
        {
            _interceptor.DisposeEvent();
        }
    }
}
