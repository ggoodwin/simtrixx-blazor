using Common.Settings;

namespace Client.Infrastructure.Settings
{
    public record ClientPreference : IPreference
    {
        public bool IsDarkMode { get; set; }
        public bool IsDrawerOpen { get; set; }
    }
}
