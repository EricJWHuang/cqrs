using System;
using System.Threading.Tasks;

namespace Cqrs.Commands.Abstract
{
    /// <summary>
    /// Command result abstract class.
    /// </summary>
    /// <typeparam name="TR">the type of Command's result</typeparam>
    internal abstract class CommandResultBase<TR> : ICommandResult, ICommandResult<TR>
    {
        void ICommandResult.SetResult(object result)
        {
            SetResult((TR)result);
        }

        /// <summary>
        /// Set command result.
        /// </summary>
        /// <param name="result">command result</param>
        public abstract void SetResult(TR result);

        /// <summary>
        /// Set command exception when error occurs.
        /// </summary>
        /// <param name="exception">exception</param>
        public abstract void SetException(Exception exception);

        /// <summary>
        /// Task (like jQuery Promise) of command's result.
        /// </summary>
        public abstract Task<TR> Result { get; }
    }
}
