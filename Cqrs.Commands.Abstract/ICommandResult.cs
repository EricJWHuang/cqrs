using System;
using System.Threading.Tasks;

namespace Cqrs.Commands.Abstract
{
    /// <summary>
    /// Generic command result interface.
    /// </summary>
    internal interface ICommandResult
    {
        // This interface is introduced to make a set of ICommandResult<TR> possibile
        //  See CommandDispatcher for its usage
        //  This is inspired by http://stackoverflow.com/a/8877622/2462104

        /// <summary>
        /// Set command result.
        /// </summary>
        /// <param name="result">command result</param>
        void SetResult(object result);

        /// <summary>
        /// Set command exception when error occurs.
        /// </summary>
        /// <param name="exception">exception</param>
        void SetException(Exception exception);
    }

    /// <summary>
    /// Command result interface that returns a result of type <typeparamref name="TR"/>. It represents result of ICommand<TR>.
    /// </summary>
    /// <typeparam name="TR"></typeparam>
    public interface ICommandResult<TR>
    {
        // It works with ICommand<TR>

        // This public facing interface is designed to expose a command's result
        Task<TR> Result { get; }
    }
}
