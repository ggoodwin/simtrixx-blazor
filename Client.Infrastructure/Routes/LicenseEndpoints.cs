namespace Client.Infrastructure.Routes
{
    public class LicenseEndpoints
    {
        public static string DownloadFileFiltered(string searchString)
        {
            return $"{DownloadFile}?searchString={searchString}";
        }

        public static string GetAllLicenses = "api/license/all";
        public static string Import = "api/license/import";
        public static string Save = "api/license";
        public static string Delete = "api/license";
        public static string DownloadFile = "api/license/export";
        public static string GetLicenseByUser = "api/license/user";
        public static string GetLicense(int licenseId)
        {
            return $"api/license/{licenseId}";
        }
    }
}
