using BL.Options;
using BL.Services;
using Extensions.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Reflection;

namespace BL
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterBL(this IServiceCollection serviceCollection)
        {
            return serviceCollection.RegisterServices(ServiceLifetime.Transient);
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

        public static IServiceCollection AddFileSettingOption(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var fileSettingOptions = new FileSettingOptions();
            serviceCollection.Configure<FileSettingOptions>(x =>
                configuration.GetSection(FileSettingOptions.SectionName)
            );
            return serviceCollection;
        }

        public static IServiceCollection AddEmailing(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var emailSettingOptions = new EmailSettingOptions();
            serviceCollection.Configure<EmailSettingOptions>( x =>
                configuration.GetSection(EmailSettingOptions.SectionName)
            );
            return serviceCollection;
        }
    }
}
