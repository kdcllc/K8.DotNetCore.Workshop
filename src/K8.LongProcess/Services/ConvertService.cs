using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace K8.LongProcess.Services
{
    public class ConvertService : IProcessService
    {
        private readonly ILogger<ConvertService> _logger;
        private readonly IConfiguration _configuration;

        public ConvertService(IConfiguration configuration, ILogger<ConvertService> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> ExecuteAsync(int count, int delayTime, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            for (var i = 0; i < count; i++)
            {
                _logger.LogInformation(
                    "Executing {serviceName} - {count}- Key Vault - {keyValue}",
                    nameof(ConvertService),
                    i,
                    _configuration.GetValue<string>("AzCronJobKey"));
                await Task.Delay(TimeSpan.FromSeconds(delayTime));
            }

            return await Task.FromResult(0);
        }
    }
}
