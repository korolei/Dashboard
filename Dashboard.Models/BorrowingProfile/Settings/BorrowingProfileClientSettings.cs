using Dashboard.Common.Configuration;

namespace Dashboard.Models.BorrowingProfile.Settings
{
    public interface IBorrowingProfileClientSettings
    {
        string GetApplicationName();
    }

    public sealed class BorrowingProfileClientSettings : IBorrowingProfileClientSettings
    {
        private const string APPLICATION_NAME_KEY = "BorrowingProfile.ApplicationName";
        private const string ENVIRONMENT = "Environment";

        public string ApplicationName { get; set; }

        public BorrowingProfileClientSettings()
        {
            ApplicationName = GetApplicationName();
        }

        public string GetApplicationName()
        {
            return $"{GlobalConfigurationSettings.GetSettings(APPLICATION_NAME_KEY)}-{GlobalConfigurationSettings.GetSettings(ENVIRONMENT)}";
        }
    }
}