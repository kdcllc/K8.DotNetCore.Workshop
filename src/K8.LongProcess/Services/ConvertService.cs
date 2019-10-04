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

        public async Task<int> ExecuteAsync(int count, int delayTime, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            for (var i = 0; i < count; i++)
            {
                _logger.LogInformation("Executing {serviceName} - {count}", nameof(ConvertService), i);
                await Task.Delay(TimeSpan.FromSeconds(delayTime));
            }

            return await Task.FromResult(0);
        }
    }
}
