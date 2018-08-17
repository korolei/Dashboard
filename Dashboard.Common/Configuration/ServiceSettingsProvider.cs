using System;
using Dashboard.Common.Connected_Services.SettingsProvider;
using Dashboard.Common.Services;
using Microsoft.Extensions.Configuration;

namespace Dashboard.Common.Configuration
{
    public class ServiceSettingsProvider : ConfigurationProvider
    {
        private readonly AppSettings _settings;

        public ServiceSettingsProvider(AppSettings settings)
        {
            _settings = settings;
        }

        public override void Load()
        {
            var proxy = new GenericServiceProxy<ISettings, SettingsClient>(new Uri(_settings.ConfigurationServiceUrl));
            Data = proxy.Execute(client => client.GetApplicationSettingsAsync(_settings.ApplicationNamespace)).Result;
            GlobalConfigurationSettings.MossySettings = Data;
        }
    }
}