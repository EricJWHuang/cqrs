using System;
using System.Threading.Tasks;

namespace Cqrs.Commands.Abstract
{
    /// <summary>
    /// ICommandResult<TR> base class.
    /// </summary>
    /// <typeparam name="TR"></typeparam>
    internal class CommandResult<TR> : CommandResultBase<TR>
    {
        private readonly TaskCompletionSource<TR> _tcs;

        public CommandResult()
        {
            _tcs = new TaskCompletionSource<TR>();
        }

        public override void SetResult(TR result)
        {
            _tcs.SetResult(result);
        }

        public override void SetException(Exception exception)
        {
            _tcs.SetException(exception);
        }

        public override Task<TR> Result
        {
            get { return _tcs.Task; }
        }
    }
}
