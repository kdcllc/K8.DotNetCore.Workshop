using System;
using System.Collections.Generic;
using K8.AspNetCore.HealthChecks.AzureBlobStorage;
using K8.AspNetCore.HealthChecks.SigtermCheck;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace K8.AspNetCore.HealthChecks
{
    public static class HealthCheckBuilderExtensions
    {
        public static IHealthChecksBuilder AddAzureBlobStorageCheck(
            this IHealthChecksBuilder builder,
            string name,
            string containerName,
            Action<StorageAccountOptions> setup,
            HealthStatus? failureStatus = default,
            IEnumerable<string> tags = default)
        {
            var options = new StorageAccountOptions();
            setup?.Invoke(options);

            builder.Services.AddOptions<StorageAccountOptions>(name).Configure((opt) => opt = options);

            builder.AddCheck<AzureBlobStorageHealthCheck>(name, failureStatus ?? HealthStatus.Degraded, tags);

            return builder;
        }

        /// <summary>
        /// Add SIGTERM Healcheck that provides notification for orchestrator with unhealthy status once the application begins to shut down.
        /// </summary>
        /// <param name="builder">The <see cref="IHealthChecksBuilder"/>.</param>
        /// <param name="name">The name of the HealthCheck.</param>
        /// <param name="failureStatus">The <see cref="HealthStatus"/>The type should be reported when the health check fails. Optional. If <see langword="null"/> then.</param>
        /// <param name="tags">A list of tags that can be used to filter sets of health checks. Optional.</param>
        /// <returns></returns>
        public static IHealthChecksBuilder AddSigtermCheck(
            this IHealthChecksBuilder builder,
            string name,
            HealthStatus? failureStatus = null,
            IEnumerable<string> tags = default)
        {
            builder.AddCheck<SigtermHealthCheck>(name, failureStatus, tags);

            return builder;
        }
    }
}
