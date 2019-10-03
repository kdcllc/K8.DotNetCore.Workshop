using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace K8.LongProcess.Services
{
    public class ConvertService : IProcessService
    {
        private readonly ILogger<ConvertService> _logger;

        public ConvertService(ILogger<ConvertService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<int> ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Executing {serviceName}", nameof(ConvertService));
            return Task.FromResult(0);
        }
    }
}
