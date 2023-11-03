using Serilog.Events;

namespace WebApiTemplate.SharedKernel.Interfaces
{
    /// <summary>
    /// Represents an interface for custom logging using Serilog.
    /// </summary>
    public interface ICustomLogger
    {
        /// <summary>
        /// Logs a message with an optional exception using Serilog.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="level">The log event level (default is Information).</param>
        /// <param name="ex">An optional exception to include in the log.</param>
        void LogMessage(string message = null, LogEventLevel level = LogEventLevel.Information, Exception ex = null);
    }
}
