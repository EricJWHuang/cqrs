using System;

namespace Cqrs.Commands.Abstract.Exceptions
{
    [Serializable]
    public class CommandExecutorNotFoundException : Exception
    {
        public CommandExecutorNotFoundException(ICommand command)
            : base(string.Format("No executor found for command {0}.", command.GetType().Name))
        {
        }
    }
}
