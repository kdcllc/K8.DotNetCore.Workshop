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

        public Task<int> ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Executing {serviceName}", nameof(ImportService));
            return Task.FromResult(0);
        }
    }
}
