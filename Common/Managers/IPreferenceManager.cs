using Common.Settings;

namespace Common.Managers
{
    public interface IPreferenceManager
    {
        Task SetPreference(IPreference preference);
        Task<IPreference> GetPreference();
    }
}
