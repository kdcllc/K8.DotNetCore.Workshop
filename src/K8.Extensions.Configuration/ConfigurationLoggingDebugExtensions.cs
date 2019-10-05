using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.Configuration
{
    public static class ConfigurationLoggingDebugExtensions
    {
        /// <summary>
        /// Displays all of the application configurations based on the Configuration Provider.
        /// </summary>
        /// <param name="config"></param>
        public static void DebugConfigurations(this IConfigurationRoot config)
        {
            using (var logFactory = GetLoggerFactory())
            {
                var logger = logFactory.CreateLogger("Program");
                var allConfigurations = config.GetDebugView();
                logger.LogDebug(allConfigurations);
            }
        }

        private static ILoggerFactory GetLoggerFactory()
        {
            return LoggerFactory.Create(builder =>
            {
                builder.AddDebug();
                builder.AddConsole();
                builder.AddFilter((_) => true);
            });
        }
    }
}
