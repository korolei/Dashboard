using System.Collections.Generic;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Dashboard.Common.Logging
{
    public static class ApplicationLogging
    {
        public static Logger GetSeriLogger(Dictionary<string, string> mossySettings)
        {
            var environment = mossySettings[ApplicationSettings.EnvironmentName];
            var appName = mossySettings[ApplicationSettings.ApplicationName];
            if (environment.ToUpper() == "DEV")
            {
                return new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .WriteTo.EventLog(appName)
                    .MinimumLevel.Error()
                    .CreateLogger();
            }
            else
            {
                return new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                    .Enrich.FromLogContext()
                    //.WriteTo.MSSqlServer(ApplicationSettings.MossyDatabase,"EventLog",LogEventLevel.Error,10)
                    //.WriteTo.Email(
                    //    fromEmail: "Portal Event Log <PortalEventLog@ofina.on.ca>",
                    //    toEmail: mossySettings[ApplicationSettings.EventLogRecipients],
                    //    mailServer: mossySettings[ApplicationSettings.ExchangeServerUrl],
                    //    mailSubject: string.Format(CultureInfo.InvariantCulture,
                    //        ": A failure has occurred in the {0} application", appName)
                    //    )
                    .CreateLogger();
            }
        }
    }
}
