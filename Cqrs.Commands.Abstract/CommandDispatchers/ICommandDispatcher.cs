namespace Cqrs.Commands.Abstract.CommandDispatchers
{
    public interface ICommandDispatcher
    {
        /// <summary>
        /// Dispatch a command and its result will be ready later (just like jQuery Promise).
        /// </summary>
        /// <typeparam name="TR">the type of Command's result</typeparam>
        /// <param name="command">command</param>
        /// <returns>command's result so that caller can await for</returns>
        ICommandResult<TR> Dispatch<TR>(ICommand<TR> command);
    }
}
