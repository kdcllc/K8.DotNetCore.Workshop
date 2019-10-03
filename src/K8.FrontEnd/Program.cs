using System;
using K8.FrontEnd.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace K8.FrontEnd
{
    internal sealed class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    // works together with SigtermCheck
                    webBuilder.UseShutdownTimeout(TimeSpan.FromSeconds(30));
                    webBuilder.ConfigureServices(services =>
                    {
                        services.AddHostedService<DelayService>();
                    });
                });
        }
    }
}
