using System.Diagnostics;
using System.Threading.Tasks;
using Cqrs.Commands.Abstract.CommandExecutors;
using Cqrs.Commands.Abstract.Exceptions;
using Cqrs.Commands.Abstract.Logging;

namespace Cqrs.Commands.Abstract.CommandDispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly ICommandExecutorResolver _commandExecutorResolver;
        private readonly ILogger _logger;

        public CommandDispatcher(ICommandExecutorResolver commandExecutorResolver,
            ILogger logger)
        {
            _commandExecutorResolver = commandExecutorResolver;
            _logger = logger;
        }

        public async Task<TR> DispatchAsync<TR>(ICommand<TR> command)
        {
            _logger.Verbose(string.Format("Received {0}", command.GetType().Name));

            var watch = Stopwatch.StartNew();

            var commandExecutor = _commandExecutorResolver.GetCommandExecutor(command);
            if (commandExecutor == null)
                throw new CommandExecutorNotFoundException(command);

            var result = (TR)await commandExecutor.ExecuteAsync(command);

            watch.Stop();

            _logger.Verbose(string.Format("Took {0} ms", watch.ElapsedMilliseconds));

            return result;
        }
    }
}
