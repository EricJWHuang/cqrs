namespace Cqrs.Commands.Abstract.CommandExecutors
{
    /// <summary>
    /// Command executor resolver interface.
    /// </summary>
    public interface ICommandExecutorResolver
    {
        // It behaves like a DependencyResolver

        /// <summary>
        /// Get a command executor for a given command.
        /// </summary>
        /// <param name="command">command</param>
        /// <returns>a command executor</returns>
        ICommandExecutor GetCommandExecutor(ICommand command);
    }
}
