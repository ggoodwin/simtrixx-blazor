using Common.Managers;
using MudBlazor;

namespace Client.Infrastructure.Managers.Preferences
{
    public interface IClientPreferenceManager : IPreferenceManager
    {
        Task<MudTheme> GetCurrentThemeAsync();
        Task<bool> ToggleDarkModeAsync();
    }
}
