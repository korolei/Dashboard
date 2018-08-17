using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Dashboard.Common.Configuration
{
    public class MossySettingsProvider : ConfigurationProvider
    {
        private static AppSettings _settings;

        public MossySettingsProvider(AppSettings settings)
        {
            _settings = settings;
        }

        public override void Load()
        {
            Data = LoadSettings(_settings.ApplicationNamespace);
            GlobalConfigurationSettings.MossySettings = Data;
        }

        private static Dictionary<string, string> LoadSettings(string hashName)
        {
            var configValues = new Dictionary<string, string>();
            using (var conn = new SqlConnection(_settings.MossyDatabase))
            {
                using (var command = new SqlCommand("[dbo].[GetApplicationSettings]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@HashName", hashName ?? string.Empty));
                    conn.Open();

                    using (var reader = command.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            configValues.Add(reader["SettingKey"].ToString(), reader["SettingValue"].ToString());
                        }
                    }
                }
            }
            return configValues;
        }
    }
}
