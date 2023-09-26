using Blazored.LocalStorage;
using Client.Infrastructure.Settings;
using Common.Constants.Storage;
using Common.Settings;
using MudBlazor;

namespace Client.Infrastructure.Managers.Preferences
{
    public class ClientPreferenceManager : IClientPreferenceManager
    {
        private readonly ILocalStorageService _localStorageService;

        public ClientPreferenceManager(
            ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task<bool> ToggleDarkModeAsync()
        {
            if (await GetPreference() is not ClientPreference preference) return false;
            preference.IsDarkMode = !preference.IsDarkMode;
            await SetPreference(preference);
            return !preference.IsDarkMode;

        }

        public async Task<MudTheme> GetCurrentThemeAsync()
        {
            if (await GetPreference() is not ClientPreference preference) return SimtrixxTheme.DefaultTheme;
            return preference.IsDarkMode ? SimtrixxTheme.DarkTheme : SimtrixxTheme.DefaultTheme;
        }

        public async Task<IPreference> GetPreference()
        {
            return await _localStorageService.GetItemAsync<ClientPreference>(StorageConstants.Local.Preference) ?? new ClientPreference();
        }

        public async Task SetPreference(IPreference preference)
        {
            await _localStorageService.SetItemAsync(StorageConstants.Local.Preference, preference as ClientPreference);
        }
    }
}
