using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Abstractions;

namespace K8.Extensions.Hosting.Internal
{
    /// <summary>
    ///     A DI container for storing command line arguments.
    /// </summary>
    internal class CommandLineState : CommandLineContext
    {
        public CommandLineState(string[] args)
        {
            Arguments = args;
        }

        public int ExitCode { get; set; }

        internal void SetConsole(IConsole console)
        {
            Console = console;
        }
    }
}
