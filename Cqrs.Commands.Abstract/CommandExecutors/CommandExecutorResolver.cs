using System.Collections.Generic;
using System.Linq;

namespace Cqrs.Commands.Abstract.CommandExecutors
{
    /// <summary>
    /// Default command executor resolver that has a reference to a set of ICommandExecutor.
    /// </summary>
    public class CommandExecutorResolver : ICommandExecutorResolver
    {
        // check out http://stackoverflow.com/a/8877622/2462104 when it comes to storing a set of generic interfaces

        private readonly IEnumerable<ICommandExecutor> _commandExecutors;

        public CommandExecutorResolver(IEnumerable<ICommandExecutor> commandExecutors)
        {
            _commandExecutors = commandExecutors;
        }

        public ICommandExecutor GetCommandExecutor(ICommand command)
        {
            return _commandExecutors.SingleOrDefault(commandExecutor => commandExecutor.IsExecutable(command));
        }
    }
}
