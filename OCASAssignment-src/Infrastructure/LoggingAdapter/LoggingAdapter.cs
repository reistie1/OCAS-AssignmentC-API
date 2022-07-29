using OCASAPI.Application.Interfaces;

namespace OCASAPI.Infrastructure.LoggingAdapter
{
    /// <summary>
    /// LoggerAdapter abstraction inherits from <see cref="Microsoft.Extensions.Logging" /> for logging
    /// information and warnings that occur during application run.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LoggerAdapter<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;
        public LoggerAdapter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }
    }
}