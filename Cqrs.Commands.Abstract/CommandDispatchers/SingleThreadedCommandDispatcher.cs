using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Cqrs.Commands.Abstract.CommandExecutors;
using Cqrs.Commands.Abstract.Exceptions;
using Cqrs.Commands.Abstract.Logging;

namespace Cqrs.Commands.Abstract.CommandDispatchers
{
    public class SingleThreadedSingleThreadedCommandDispatcher : ISingleThreadedCommandDispatcher
    {
        // inspired by https://ayende.com/blog/167299/async-event-loops-in-c
        // This class uses producer-consumer pattern where
        //  Dispatch method is responsible for producing commands into an in-memory queue
        //  A dedicate SINGLE thread is responsible for consuming those commands from the in-memory queue

        // check out http://stackoverflow.com/a/8877622/2462104 when it comes to storing a set of generic interfaces

        private readonly BufferBlock<IQueueItem> _commandQueue = new BufferBlock<IQueueItem>();

        // This class is meant to be setup as singleton in IoC

        private interface IQueueItem
        {
            ICommand Command { get; }
            ICommandResult CommandResult { get; }
        }

        private class QueueItem<TR> : IQueueItem
        {
            public QueueItem(ICommand<TR> command, CommandResultBase<TR> commandResult)
            {
                Command = command;
                CommandResult = commandResult;
            }

            private ICommand<TR> Command { get; set; }
            private CommandResultBase<TR> CommandResult { get; set; }

            ICommand IQueueItem.Command
            {
                get { return Command; }
            }

            ICommandResult IQueueItem.CommandResult
            {
                get { return CommandResult; }
            }
        }

        private readonly ICommandExecutorResolver _commandExecutorResolver;
        private readonly ILogger _logger;

        public SingleThreadedSingleThreadedCommandDispatcher(ICommandExecutorResolver commandExecutorResolver,
            ILogger logger)
        {
            _commandExecutorResolver = commandExecutorResolver;
            _logger = logger;

            Initialise();
        }

        /// <summary>
        /// Initialise command loop.
        /// </summary>
        private void Initialise()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    _logger.Verbose("WaitCommand");

                    // command is typeof QueueItem<TR>
                    IQueueItem queueItem = null;
                    try
                    {
                        // wait for a command indefinitely
                        queueItem = _commandQueue.Receive(TimeSpan.FromMilliseconds(-1));

                        // Convert.ChangeType and typeof(ICommand<>).MakeGenericType can be used to convert the object into strongly typed ICommand<> and ICommandResult<>
                        //  but it is not worth doing it

                        var command = queueItem.Command;
                        var commandResult = queueItem.CommandResult;

                        _logger.Verbose(string.Format("Received {0}", command.GetType().Name));
                        _logger.Verbose(string.Format("{0} commands waiting for process", _commandQueue.Count));

                        // process command
                        var watch = Stopwatch.StartNew();

                        var result = ProcessCommand(command).Result;
                        commandResult.SetResult(result);

                        watch.Stop();

                        _logger.Verbose(string.Format("Took {0} ms", watch.ElapsedMilliseconds));
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(string.Format("Failed due to {0}", ex));

                        if (queueItem != null
                            && queueItem.CommandResult != null)
                        {
                            queueItem.CommandResult.SetException(ex);
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Dispatch a command and its result will be ready later (just like jQuery Promise).
        /// </summary>
        /// <typeparam name="TR">the type of Command's result</typeparam>
        /// <param name="command">command</param>
        /// <returns>command's result so that caller can await for</returns>
        public ICommandResult<TR> Dispatch<TR>(ICommand<TR> command)
        {
            // The idea is that when a command is submitted, a ICommandResult<TR> is returned so that consumers are await for it

            // associate a result with it and add the command into queue
            var commandResult = new CommandResult<TR>();
            _commandQueue.Post(new QueueItem<TR>(command, commandResult));

            return commandResult;
        }

        /// <summary>
        /// Process a command.
        /// </summary>
        /// <param name="command">command</param>
        /// <returns>command's result</returns>
        private async Task<object> ProcessCommand(ICommand command)
        {
            var commandExecutor = _commandExecutorResolver.GetCommandExecutor(command);
            if (commandExecutor == null)
                throw new CommandExecutorNotFoundException(command);

            return await commandExecutor.ExecuteAsync(command);
        }
    }
}
