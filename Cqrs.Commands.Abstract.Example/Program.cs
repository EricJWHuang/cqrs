using System;
using Cqrs.Commands.Abstract.CommandDispatchers;
using Cqrs.Commands.Abstract.CommandExecutors;
using Cqrs.Commands.Abstract.Example.CommandExecutors;
using Cqrs.Commands.Abstract.Example.Commands;
using Cqrs.Commands.Abstract.Logging;

namespace Cqrs.Commands.Abstract.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // IoC container could be used to set up the SingleThreadCommandDispatcher's dependencies
            var commandExecutor = new InitialiseCommandExecutor();
            var commandExecutorResolver = new CommandExecutorResolver(new[] {commandExecutor});
            var logger = new SystemDiagnosticsLogger();

            // Example: process 10 InitialiseCommand one-by-one
            var commandDispatcher = new SingleThreadCommandDispatcher(commandExecutorResolver, logger);

            for (var i = 0; i < 10; i++)
            {
                commandDispatcher.Dispatch(new InitialiseCommand("test repo " + i));
            }

            Console.ReadLine();
        }
    }
}
