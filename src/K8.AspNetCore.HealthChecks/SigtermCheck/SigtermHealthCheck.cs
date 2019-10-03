using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace K8.AspNetCore.HealthChecks.SigtermCheck
{
    public class SigtermHealthCheck : IHealthCheck
    {
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly ILogger<SigtermHealthCheck> _logger;

        public SigtermHealthCheck(IHostApplicationLifetime applicationLifetime, ILogger<SigtermHealthCheck> logger)
        {
            _applicationLifetime = applicationLifetime ?? throw new System.ArgumentNullException(nameof(applicationLifetime));

            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
            _applicationLifetime.ApplicationStopping.Register(() =>
            {
                _logger.LogWarning("Application Stopping");
            });
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            if (_applicationLifetime.ApplicationStopping.IsCancellationRequested)
            {
                return Task.FromResult(new HealthCheckResult(
                      context.Registration.FailureStatus,
                      description: "IHostApplicationLifetime.ApplicationStopping was requested"));
            }

            return Task.FromResult(HealthCheckResult.Healthy());
        }
    }
}
