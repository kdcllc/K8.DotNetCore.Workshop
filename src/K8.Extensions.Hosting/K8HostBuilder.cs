using System;
using System.IO;

using Microsoft.Extensions.Configuration;

using Serilog;

namespace Microsoft.Extensions.Hosting
{
    public static class K8HostBuilder
    {
        public static IHostBuilder CreateDefaultBuilder()
        {
            return CreateDefaultBuilder(args: null);
        }

        /// <summary>
        /// Adds <see cref="!:AddJsonFile"/> for configuration of the appsettings.json.
        /// Adds <see cref="!:AddEnvironmentVariables"/> for Environment variables.
        /// </summary>
        /// <returns></returns>
        public static IHostBuilder CreateDefaultBuilder(string[] args)
        {
            var builder = new HostBuilder();

            builder.UseContentRoot(Directory.GetCurrentDirectory());

            builder.ConfigureHostConfiguration(config =>
            {
                config.AddEnvironmentVariables(prefix: "DOTNET_");
                if (args != null)
                {
                    config.AddCommandLine(args);
                }
            });

            builder.UseEnvironment(Environment.GetEnvironmentVariable("ENVIRONMENT") ?? "Production");

            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;

                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                config.AddEnvironmentVariables();

                if (args != null)
                {
                    config.AddCommandLine(args);
                }
            });

            builder.ConfigureAppConfiguration((hostingContext, configBuilder) =>
            {
                configBuilder.UseKeyVaultConfiguration(hostingContext.HostingEnvironment.IsDevelopment());
            });

            return builder;
        }
    }
}
