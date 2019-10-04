using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using K8.LongProcess.Services;

using McMaster.Extensions.CommandLineUtils;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace K8.LongProcess
{
    internal class Program
    {
        private readonly ILogger<Program> _logger;
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly Func<ProcessType, IProcessService> _dataFactory;

        public Program(
            ILogger<Program> logger,
            IHostApplicationLifetime applicationLifetime,
            Func<ProcessType, IProcessService> dataFactory)
        {
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
            _applicationLifetime = applicationLifetime ?? throw new System.ArgumentNullException(nameof(applicationLifetime));
            _dataFactory = dataFactory ?? throw new ArgumentNullException(nameof(dataFactory));
        }

        [Option("-t", Description ="Specifies the type of process to perform.")]
        [Required]
        public ProcessType ServiceType { get; set; }

        [Option("-d", Description = "Specifies the delay time for the job in seconds.")]
        [Required]
        public int DelayTime { get; set; }

        [Option("-c", Description = "Specifies the count iteration for the job.")]
        [Required]
        public int Count { get; set; }

        public async Task OnExecuteAsync()
        {
            try
            {
                _logger.LogInformation("Running the long running data process: {processType}", ServiceType);
                await _dataFactory(ServiceType).ExecuteAsync(Count, DelayTime, _applicationLifetime.ApplicationStopping);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred running {processType}", ServiceType);
                throw;
            }
        }

        private static async Task<int> Main(string[] args)
        {
            // creates custom HostBuilder
            var hostBuilder = K8HostBuilder.CreateDefaultBuilder(args);

            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddScoped<ConvertService>();
                services.AddScoped<ImportService>();

                // register the process services.
                services.AddScoped<Func<ProcessType, IProcessService>>(sp =>
                serviceType =>
                {
                    return serviceType switch
                    {
                        ProcessType.Convert => sp.GetRequiredService<ConvertService>(),
                        ProcessType.Import => sp.GetRequiredService<ImportService>(),
                        _ => null,
                    };
                });
            });

            // runs and processes the command-line parameters
            return await hostBuilder.RunCommandLineAsync<Program>(args);
        }
    }
}
