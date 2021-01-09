using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;

namespace Function
{
    internal class SerilogLoggingProvider : ILoggerProvider
    {
        private ILoggerFactory loggerFactory;

        public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName)
        {
            if(loggerFactory == null)
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.Console()
                    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                    .CreateLogger();
                loggerFactory = new SerilogLoggerFactory(null, true, null);
            }
            return loggerFactory.CreateLogger(categoryName);
        }

        public void Dispose()
        {
            loggerFactory.Dispose();
        }
    }
}