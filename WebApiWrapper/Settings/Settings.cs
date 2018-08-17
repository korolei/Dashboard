using System;

namespace WebApiWrapper.Settings
{
    public static class Settings
    {
        private const string PART_NAME = MossyApplicationNames.BORROWING_PROFILE_NAME;

        public static Uri BorrowingProfileProviderUrl => new Uri(SettingsHelper.AppSetting("DataProviderUrl", PART_NAME));
    }
}