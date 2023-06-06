using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Logging
{
    public static class ServiceRegistration
    {
        public static ILoggingBuilder AddFileLogger(
        this ILoggingBuilder builder, IConfiguration configuration)
        {
            var logFolder = configuration["logFolder"];
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File(
                $"{logFolder}log-{DateTime.Now:yyyy-MM-dd}.txt",
                rollingInterval: RollingInterval.Day,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                retainedFileCountLimit: 10,
                fileSizeLimitBytes: 100 * 10^6) //100 MB
            .CreateLogger();
            builder.AddSerilog(Log.Logger);

            return builder;
        }
    }
}
