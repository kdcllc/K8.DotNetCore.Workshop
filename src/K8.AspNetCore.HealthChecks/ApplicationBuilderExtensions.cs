﻿using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Enable usage of the basic liveness check that returns 200 http status code.
        /// Default registered health check is self.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="healthCheckPath"></param>
        /// <param name="healthCheckOptions"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseLivenessHealthCheck(
            this IApplicationBuilder builder,
            string healthCheckPath = "/liveness",
            HealthCheckOptions healthCheckOptions = default)
        {
            if (healthCheckOptions == default)
            {
                // Exclude all checks and return a 200-Ok. Default registered health check is self.
                healthCheckOptions = new HealthCheckOptions { Predicate = (p) => false };
            }

            builder.UseHealthChecks(healthCheckPath, healthCheckOptions);

            return builder;
        }

        public static IApplicationBuilder UseHealthyHealthCheck(
            this IApplicationBuilder builder,
            string healthCheckPath = "/healthy",
            HealthCheckOptions healthCheckOptions = default)
        {
            if (healthCheckOptions == default)
            {
                healthCheckOptions = new HealthCheckOptions { ResponseWriter = WriteResponse };
            }

            builder.UseHealthChecks(healthCheckPath, healthCheckOptions);

            return builder;
        }

        /// <summary>
        /// Custom HealthCheck <see cref="HealthReport"/> renderer.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Task WriteResponse(
            HttpContext httpContext,
            HealthReport result)
        {
            httpContext.Response.ContentType = "application/json";

            var json = new JObject(
                new JProperty("status", result.Status.ToString()),
                new JProperty("results", new JObject(result.Entries.Select(pair =>
                    new JProperty(pair.Key, new JObject(
                        new JProperty("status", pair.Value.Status.ToString()),
                        new JProperty("description", pair.Value.Description),
                        new JProperty("data", new JObject(pair.Value.Data.Select(
                            p => new JProperty(p.Key, p.Value))))))))));

            return httpContext.Response.WriteAsync(
                json.ToString(Formatting.Indented));
        }
    }
}
