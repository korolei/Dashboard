using System;
using Microsoft.Extensions.Configuration;

namespace Dashboard.Common.Configuration
{
    public static class SettingsProviderFactory
    {
        private static AppSettings _appSettings;

        public static IConfigurationBuilder AddCustomConfig(
            this IConfigurationBuilder builder, AppSettings setup)
        {
            _appSettings = setup;
            return builder.Add(GetProvider());
        }

        public static IConfigurationSource GetProvider()
        {
            IConfigurationSource provider = null;
            if (!string.IsNullOrEmpty(_appSettings.SettingProvider))
            {
                var typeClass = Type.GetType(_appSettings.SettingProvider);
                provider = (IConfigurationSource) Activator.CreateInstance(
                    typeClass ?? throw new InvalidOperationException(
                        $"Could not get type from {_appSettings.SettingProvider} setting provider."),_appSettings
                        );
            }

            if (provider == null &&
                !string.IsNullOrEmpty(_appSettings.ConfigurationServiceUrl) &&
                !string.IsNullOrEmpty(_appSettings.ApplicationNamespace))
                provider = new ConfigurationServiceSettingsSource(_appSettings);
            else if (provider == null &&
                     !string.IsNullOrEmpty(_appSettings.MossyDatabase))
                provider = new MossySettingsProviderSource(_appSettings);
            return provider;
        }
    }

    public class MossySettingsProviderSource : IConfigurationSource
    {
        private readonly AppSettings _settings;

        public MossySettingsProviderSource(AppSettings appSettings)
        {
            _settings = appSettings;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new MossySettingsProvider(_settings);
        }
    }

    public class ConfigurationServiceSettingsSource : IConfigurationSource
    {
        private readonly AppSettings _settings;

        public ConfigurationServiceSettingsSource(AppSettings settings)
        {
            _settings = settings;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ServiceSettingsProvider(_settings);
        }
    }
}