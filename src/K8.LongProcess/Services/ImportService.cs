using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace K8.LongProcess.Services
{
    public class ImportService : IProcessService
    {
        private readonly ILogger<ImportService> _logger;

        public ImportService(ILogger<ImportService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> ExecuteAsync(int count, int delayTime, CancellationToken cancellationToken)
        {
            for (var i = 0; i < count; i++)
            {
               _logger.LogInformation("Executing {serviceName} - {count}", nameof(ImportService), i);
               await Task.Delay(TimeSpan.FromSeconds(delayTime));
            }

            return await Task.FromResult(0);
        }
    }
}
