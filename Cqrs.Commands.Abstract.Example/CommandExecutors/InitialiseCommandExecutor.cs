using System;
using System.Threading;
using System.Threading.Tasks;
using Cqrs.Commands.Abstract.CommandExecutors;
using Cqrs.Commands.Abstract.Example.Commands;

namespace Cqrs.Commands.Abstract.Example.CommandExecutors
{
    public class InitialiseCommandExecutor : CommandExecutor<InitialiseCommand, Guid>
    {
        public override bool IsExecutable(ICommand<Guid> command)
        {
            return command is InitialiseCommand;
        }

        public override Task<Guid> ExecuteAsync(InitialiseCommand command)
        {
            Console.WriteLine("InitialiseCommandExecutor - {0}", command.Name);

            // say a command takes 20 ms to run
            Thread.Sleep(20);
            return Task.FromResult(Guid.NewGuid());
        }
    }
}
