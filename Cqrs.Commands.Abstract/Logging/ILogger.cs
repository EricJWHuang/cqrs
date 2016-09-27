namespace Cqrs.Commands.Abstract.Logging
{
    public interface ILogger
    {
        // The reason why this interface is defined is to support the integration with logging services like Sumo Logic
        //  which has their own native support for the integration with existing frameworks like NLog

        void Verbose(string message);
        void Error(string message);
    }
}
