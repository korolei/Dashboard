using Dashboard.Common.Configuration;

namespace Dashboard.Models
{
    public class GlobalSettings
    {
        private const string WEB_API_WRAPPER_URL_KEY ="WebApiWrapperUrl";
        public static string WebApiWrapperUrl => GlobalConfigurationSettings.GetSettings(WEB_API_WRAPPER_URL_KEY);
        public static string BorrowingProfileProviderUrl => GlobalConfigurationSettings.GetSettings("DataProviderUrl" + "." + "BorrowingProfile");
    }
}