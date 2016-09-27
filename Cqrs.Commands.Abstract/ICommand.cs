namespace Cqrs.Commands.Abstract
{
    /// <summary>
    /// Generic command interface.
    /// </summary>
    public interface ICommand
    {
        // This interface is introduced to make a set of ICommand<TR> possibile
        //  See CommandDispatcher for its usage
        //  This is inspired by http://stackoverflow.com/a/8877622/2462104
    }

    /// <summary>
    /// Command interface that expects a result of type <typeparamref name="TR"/>.
    /// </summary>
    /// <typeparam name="TR">the type of Command's result</typeparam>
    public interface ICommand<TR> : ICommand
    {
        // It works with ICommandResult<TR>

        // The idea is
        //  After submitting a command, the caller gets a Task<TR> so that it can await/async for the result
        //  separate Command from its Result
    }
}
