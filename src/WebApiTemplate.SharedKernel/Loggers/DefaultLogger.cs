using Serilog;
using Serilog.Events;
using WebApiTemplate.SharedKernel.Interfaces;

namespace WebApiTemplate.SharedKernel.Loggers
{
    /// <summary>
    /// Provides a default implementation of the custom logger interface using Serilog.
    /// </summary>
    public class DefaultLogger : ICustomLogger
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultLogger"/> class.
        /// </summary>
        /// <param name="logger">The Serilog logger instance.</param>
        public DefaultLogger(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public void LogMessage(string message, LogEventLevel level = LogEventLevel.Information, Exception ex = null)
        {
            if (ex == null)
            {
                _logger.Write(level, message);
            }
            else
            {
                _logger.Write(level, ex, message);
            }
        }
    }
}
