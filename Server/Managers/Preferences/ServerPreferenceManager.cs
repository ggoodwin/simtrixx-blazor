using Application.Interfaces.Services.Storage;
using Common.Constants.Storage;
using Common.Settings;
using Server.Settings;

namespace Server.Managers.Preferences
{
    public class ServerPreferenceManager : IServerPreferenceManager
    {
        private readonly IServerStorageService _serverStorageService;

        public ServerPreferenceManager(
            IServerStorageService serverStorageService)
        {
            _serverStorageService = serverStorageService;
        }

        //public async Task<Common.Wrapper.IResult> ChangeLanguageAsync(string languageCode)
        //{
        //    var preference = await GetPreference() as ServerPreference;
        //    if (preference != null)
        //    {
        //        //preference.LanguageCode = languageCode;
        //        await SetPreference(preference);
        //        return new Result
        //        {
        //            Succeeded = true,
        //            Messages = new List<string> { "Server Language has been changed" }
        //        };
        //    }

        //    return new Result
        //    {
        //        Succeeded = false,
        //        Messages = new List<string> { "Failed to get server preferences" }
        //    };
        //}

        public async Task<IPreference> GetPreference()
        {
            return await _serverStorageService.GetItemAsync<ServerPreference>(StorageConstants.Server.Preference) ?? new ServerPreference();
        }

        public async Task SetPreference(IPreference preference)
        {
            await _serverStorageService.SetItemAsync(StorageConstants.Server.Preference, preference as ServerPreference);
        }
    }
}
