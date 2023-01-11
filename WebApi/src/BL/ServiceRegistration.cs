﻿using BL.Options;
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
            var emailSettingOptions = new EmailSettingOptions();
            serviceCollection.Configure<EmailSettingOptions>( x =>
                configuration.GetSection(EmailSettingOptions.SectionName)
            );
            return serviceCollection;
        }
    }
}
