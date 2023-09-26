namespace Client.Pages.Settings
{
    public partial class SiteSettings
    {
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            await LoadSettings();
            _loaded = true;
        }

        private async Task LoadSettings()
        {
            
        }
    }
}
