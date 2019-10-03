using System;
using System.Threading;
using System.Threading.Tasks;

using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Conventions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace K8.Extensions.Hosting.Internal
{
    /// <inheritdoc />
    internal class CommandLineService<T> : IDisposable, ICommandLineService where T : class
    {
        private readonly CommandLineApplication _application;
        private readonly ILogger _logger;
        private readonly CommandLineState _state;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLineService{T}"/> class.
        /// </summary>
        /// <param name="logger">A logger.</param>
        /// <param name="state">The command line state.</param>
        /// <param name="serviceProvider">The DI service provider.</param>
        public CommandLineService(
            ILogger<CommandLineService<T>> logger,
            CommandLineState state,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _state = state;

            logger.LogDebug(
                "Constructing CommandLineApplication<{type}> with args [{args}]",
                typeof(T).FullName,
                string.Join(",", state.Arguments));

            _application = new CommandLineApplication<T>(state.Console, state.WorkingDirectory, true);
            _application.Conventions
                .UseDefaultConventions()
                .UseConstructorInjection(serviceProvider);

            foreach (var convention in serviceProvider.GetServices<IConvention>())
            {
                _application.Conventions.AddConvention(convention);
            }
        }

        /// <inheritdoc />
        public async Task<int> RunAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Running");
            _state.ExitCode = await _application.ExecuteAsync(_state.Arguments, cancellationToken);
            return _state.ExitCode;
        }

        public void Dispose()
        {
            _application.Dispose();
        }
    }
}
