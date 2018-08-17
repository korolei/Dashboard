using System.Collections.Generic;

namespace Dashboard.Common.Configuration
{
	public class GlobalConfigurationSettings
	{
	    public static IDictionary<string, string> MossySettings { get; set; }

        public static string GetSettings(string key)
        {
            if (!MossySettings.ContainsKey(key))
            {
                throw new KeyNotFoundException($"{key} key does not exists in MossySettings dictionary.");
            }
            return MossySettings[key];
        }
	}
}
