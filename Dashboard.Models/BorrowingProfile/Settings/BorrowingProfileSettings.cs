namespace Dashboard.Models.BorrowingProfile.Settings
{
    public interface IBorrowingProfileSettings
    {
        string GetBorrowingProfileServiceUrl();
        string GetTargetsUrl();
        string GetHedgesUrl();
        string GetDetailsUrl();
        string GetDealsUrl();
        string GetProfileUrl();
    }

    public class BorrowingProfileSettings : IBorrowingProfileSettings
    {
        public readonly string BorrowingProfileServiceBaseUrl;

        public BorrowingProfileSettings()
        {
            BorrowingProfileServiceBaseUrl = GlobalSettings.BorrowingProfileProviderUrl;
        }

        public string GetBorrowingProfileServiceUrl()
        {
            return $"{GlobalSettings.WebApiWrapperUrl}/api/borrowing_profile";
        }

        public string GetTargetsUrl()
        {
            return $"{BorrowingProfileServiceBaseUrl}/targets/";
        }
        public string GetHedgesUrl()
        {
            return $"{BorrowingProfileServiceBaseUrl}/hedges/";
        }
        public string GetDetailsUrl()
        {
            return $"{BorrowingProfileServiceBaseUrl}/details/";
        }
        public string GetDealsUrl()
        {
            return $"{BorrowingProfileServiceBaseUrl}/deals/";
        }
        public string GetProfileUrl()
        {
            return $"{BorrowingProfileServiceBaseUrl}/profiles/";
        }
    }
}