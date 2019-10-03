using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

using K8.Extensions.Hosting.Internal;

using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Abstractions;
using McMaster.Extensions.Hosting.CommandLine;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Serilog;

namespace Microsoft.Extensions.Hosting
{
    public static class HostBuilderExtensions
    {
        /// <summary>
        ///     Runs an instance of <typeparamref name="TApp" /> using <see cref="CommandLineApplication" /> to provide
        ///     command line parsing on the given <paramref name="args" />.  This method should be the primary approach
        ///     taken for command line applications.
        /// </summary>
        /// <typeparam name="TApp">The type of the command line application implementation.</typeparam>
        /// <param name="hostBuilder">This instance.</param>
        /// <param name="args">The command line arguments.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task whose result is the exit code of the application.</returns>
        public static async Task<int> RunCommandLineAsync<TApp>(
            this IHostBuilder hostBuilder,
            string[] args,
            CancellationToken cancellationToken = default) where TApp : class
        {
            var exceptionHandler = new StoreExceptionHandler();
            var state = new CommandLineState(args);
            hostBuilder.ConfigureServices(
                (context, services)
                =>
                {
                    services
                        .TryAddSingleton<IUnhandledExceptionHandler>(exceptionHandler);
                    services
                        .AddSingleton<IHostLifetime, CommandLineLifetime>()
                        .TryAddSingleton(PhysicalConsole.Singleton);
                    services
                        .AddSingleton(provider =>
                        {
                            state.SetConsole(provider.GetService<IConsole>());
                            return state;
                        })
                        .AddSingleton<CommandLineContext>(state)
                        .AddSingleton<ICommandLineService, CommandLineService<TApp>>();
                });

            hostBuilder.UseSerilog((context, logger) =>
            {
                var configuration = context.Configuration;

                logger.ReadFrom.Configuration(configuration)
                     .Enrich.FromLogContext()
                     .WriteTo.Console();
            });

            using var host = hostBuilder.Build();
            await host.RunAsync(cancellationToken);

            if (exceptionHandler.StoredException != null)
            {
                ExceptionDispatchInfo.Capture(exceptionHandler.StoredException).Throw();
            }

            return state.ExitCode;
        }
    }
}
