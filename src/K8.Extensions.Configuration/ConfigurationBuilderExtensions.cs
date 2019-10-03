using System;

using K8.Extensions.Configuration.Internal;

using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration.AzureKeyVault;

using Serilog;

namespace Microsoft.Extensions.Configuration
{
    public static class ConfigurationBuilderExtensions
    {
        /// <summary>
        /// The default name for  Azure Vault URL within 'appsettings.json'.
        /// </summary>
        public const string AzureVaultUrl = nameof(AzureVaultUrl);

        /// <summary>
        /// Use  Azure Key Vault configurations.
        /// </summary>
        /// <param name="configBuilder">The Configuration Builder.</param>
        /// <param name="isDevelopment">The flag sets the pod name and logging of the configurations. The default is dev2 in Development.</param>
        /// <param name="sectionName">The section name for the Azure Vaults URLs. The default is 'AzureVaultUrl'.</param>
        /// <param name="reloadInterval"></param>
        public static void UseKeyVaultConfiguration(
            this IConfigurationBuilder configBuilder,
            bool isDevelopment = false,
            string sectionName = AzureVaultUrl,
            TimeSpan? reloadInterval = null)
        {
            var config = configBuilder.Build();

            var pod = HostBuilderEnvironments.GetKeyPodNamespace(isDevelopment);
            var endpoint = config.GetValue<string>(sectionName);

            if (!string.IsNullOrEmpty(endpoint))
            {
                // endpoint is based on the pod name and pod is prefix for the azure vault
                configBuilder.UseKeyVaultConfiguration(endpoint, pod, reloadInterval);
            }
            else
            {
                Log.Warning($"{sectionName} is missing in the configurations. Proceeding without Azure Vault Values.");
            }
        }

        /// <summary>
        /// Use  Azure based configurations.
        /// It supports multiple key vaults in the variable for 'keyVaultEndpoint'.
        /// It is possible to specified prex_key and key values.
        /// </summary>
        /// <param name="builder">The Configuration Builder.</param>
        /// <param name="keyVaultEndpoint">The string with comma delimited Azure Key Vaults URLs.</param>
        /// <param name="keyPrefix">The prefix to be used. If no prefix is present then non-environment values are loaded.</param>
        /// <param name="reloadInterval"></param>
        public static void UseKeyVaultConfiguration(
            this IConfigurationBuilder builder,
            string keyVaultEndpoint,
            string keyPrefix,
            TimeSpan? reloadInterval = null)
        {
            if (!string.IsNullOrEmpty(keyVaultEndpoint))
            {
                var azureServiceTokenProvider = new AzureServiceTokenProvider();
#pragma warning disable CA2000 // Dispose objects before losing scope
                var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
#pragma warning restore CA2000 // Dispose objects before losing scope

                foreach (var splitEndpoint in keyVaultEndpoint.Split(';'))
                {
                    Log.Information("Using Vault {VaultUrl} with namespace {Namespace} for Azure Key Vault configuration.", splitEndpoint, keyPrefix);

                    builder.AddAzureKeyVault(new AzureKeyVaultConfigurationOptions(splitEndpoint)
                    {
                        Client = keyVaultClient,
                        Manager = new PrefixExcludingKeyVaultSecretManager(),
                        ReloadInterval = reloadInterval
                    });

                    if (!string.IsNullOrEmpty(keyPrefix))
                    {
                        builder.AddAzureKeyVault(new AzureKeyVaultConfigurationOptions(splitEndpoint)
                        {
                            Client = keyVaultClient,
                            Manager = new PrefixKeyVaultSecretManager(keyPrefix),
                            ReloadInterval = reloadInterval
                        });
                    }
                }
            }
        }
    }
}
