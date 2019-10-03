using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace K8.FrontEnd.Services
{
    public class DelayService : IHostedService
    {
        private readonly ILogger<DelayService> _logger;

        public DelayService(ILogger<DelayService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Hosted service {serviceName} started", nameof(DelayService));
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(40));

            _logger.LogInformation("Hosted service {serviceName} Stopped", nameof(DelayService));

            await Task.CompletedTask;
        }
    }
}
