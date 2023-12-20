using BL.Options;
using BL.Services;
using BL.Services.EventLog;
using Extensions.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Serilog;
using System.Reflection;

namespace BL
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterBL(this IServiceCollection serviceCollection)
        {
            return serviceCollection.RegisterServices(ServiceLifetime.Scoped);
        }

        private static IServiceCollection RegisterServices(this IServiceCollection serviceCollection,
            ServiceLifetime dependencyLifetime)
        {
            return serviceCollection.RegisterImplementations<IService>(dependencyLifetime);
        }

        public static IEnumerable<Assembly> GetAutomapperAssemblies()
        {
            return new[] { typeof(ServiceRegistration).Assembly };
        }

        public static IServiceCollection AddEmailing(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddOptions<EmailSettingOptions>()
                .Bind(configuration.GetSection(EmailSettingOptions.SectionName));
            return serviceCollection;
        }

        public static IServiceCollection AddFileSettingOption(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddOptions<FileSettingOptions>()
                .Bind(configuration.GetSection(FileSettingOptions.SectionName));
            return serviceCollection;
        }

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
                fileSizeLimitBytes: 100 * 10 ^ 6) //100 MB
            .CreateLogger();
            builder.AddSerilog(Log.Logger);

            return builder;
        }

        public static ILoggingBuilder AddDatabaseEventLog(this ILoggingBuilder builder, IConfiguration configuration)
        {
            builder.AddConfiguration();
            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, EventLogLoggerProvider>()
            );
            LoggerProviderOptions.RegisterProviderOptions<EventLogLoggerConfiguration, EventLogLoggerProvider>(builder.Services);
            builder.Services.AddOptions<EventLogOptions>()
               .Bind(configuration.GetSection(EventLogOptions.SectionName));
            return builder;
        }
    }
}
