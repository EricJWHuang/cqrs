using System.Threading.Tasks;

namespace Cqrs.Commands.Abstract.CommandExecutors
{
    /// <summary>
    /// Generic command executor interface.
    /// </summary>
    public interface ICommandExecutor
    {
        // All command executors need to inherit from CommandExecutor<TC, TR>, rather than this one

        // This interface is introduced to make a set of CommandExecutor<TC, TR> possibile
        //  See CommandExecutorResolver for its usage
        //  This is inspired by http://stackoverflow.com/a/8877622/2462104

        /// <summary>
        /// Is the command executor able to execute the command.
        /// </summary>
        /// <param name="command">command</param>
        /// <returns></returns>
        bool IsExecutable(object command);

        /// <summary>
        /// Execute a command.
        /// </summary>
        /// <param name="command">command</param>
        /// <returns>command result</returns>
        Task<object> ExecuteAsync(object command);
    }
}
