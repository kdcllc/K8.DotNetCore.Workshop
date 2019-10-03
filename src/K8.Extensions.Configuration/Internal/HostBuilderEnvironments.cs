using System;
using System.Collections.Generic;

using Serilog;

namespace K8.Extensions.Configuration.Internal
{
    internal static class HostBuilderEnvironments
    {
        public static readonly IReadOnlyList<string> Env = new List<string> { "dev", "qa", "uat", "prod" };

        /// <summary>
        /// POD_NAMESPACE: dev.
        /// </summary>
        /// <param name="isDevelopment">Get corresponding pod name.</param>
        public static string GetKeyPodNamespace(bool isDevelopment)
        {
            var env = Environment.GetEnvironmentVariable("POD_NAMESPACE");

            if (string.IsNullOrEmpty(env))
            {
                if (!isDevelopment)
                {
                    Log.Warning("No POD_NAMESPACE environment variable was found. App will default to dev.");
                }

                return "dev";
            }

            return env;
        }
    }
}
