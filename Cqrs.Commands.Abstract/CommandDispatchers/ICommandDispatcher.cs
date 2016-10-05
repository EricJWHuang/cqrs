using System.Threading.Tasks;

namespace Cqrs.Commands.Abstract.CommandDispatchers
{
    public interface ICommandDispatcher
    {
        /// <summary>
        /// Dispatch a command asynchronous.
        /// </summary>
        /// <typeparam name="TR">the type of Command's result</typeparam>
        /// <param name="command">command</param>
        /// <returns>command's result</returns>
        Task<TR> DispatchAsync<TR>(ICommand<TR> command);
    }
}
