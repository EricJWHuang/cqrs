using System.Threading.Tasks;

namespace Cqrs.Commands.Abstract.CommandExecutors
{
    /// <summary>
    /// Command executor abstract class.
    /// </summary>
    /// <typeparam name="TC">the type of Command</typeparam>
    /// <typeparam name="TR">the type of Command's result</typeparam>
    public abstract class CommandExecutor<TC, TR> : ICommandExecutor
        where TC : ICommand<TR>
    {
        // the executor executes a command of type TC and returns a result of type TR

        // the command executor's responsiblity includes
        //  load an aggregate
        //  load the data that the aggregate requires to finish this command
        //  call aggregate's method (where the core logic is)
        //  save the aggregate state

        /// <summary>
        /// Is the command executor able to execute the command
        /// </summary>
        /// <param name="command">command</param>
        /// <returns></returns>
        public abstract bool IsExecutable(ICommand<TR> command);

        /// <summary>
        /// Execute a command
        /// </summary>
        /// <param name="command">command</param>
        /// <returns>command result</returns>
        public abstract Task<TR> ExecuteAsync(TC command);

        public bool IsExecutable(object command)
        {
            if (!(command is TC))
                return false;

            return IsExecutable((TC)command);
        }

        public async Task<object> ExecuteAsync(object command)
        {
            return await ExecuteAsync((TC)command);
        }
    }
}
