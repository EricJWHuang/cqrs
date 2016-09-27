using System.Diagnostics;

namespace Cqrs.Commands.Abstract.Logging
{
    public class SystemDiagnosticsLogger : ILogger
    {
        private static readonly TraceSource TraceSource = new TraceSource("Cqrs.Commands.Abstract.Logging");

        public void Verbose(string message)
        {
            TraceSource.TraceEvent(TraceEventType.Verbose, 0, message);
        }

        public void Error(string message)
        {
            TraceSource.TraceEvent(TraceEventType.Error, 0, message);
        }
    }
}