using Ofa.Common;

namespace WebApiWrapper.Settings
{
    public static class SettingsHelper
    {
        public static string AppSetting(string settingName, string appName)
        {
            return OfaApplication.GetApplicationSetting(appName + "." + settingName);
        }

        public static string AppSetting(string settingName)
        {
            return OfaApplication.GetApplicationSetting(settingName);
        }

        public static string ConfigSetting(string settingName, string appName)
        {
            return OfaApplication.GetConfigurationSetting(appName + "." + settingName);
        }

        public static string ConfigSetting(string settingName)
        {
            return OfaApplication.GetConfigurationSetting(settingName);
        }
    }
}