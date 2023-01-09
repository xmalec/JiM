using Extensions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Extensions.Extensions
{
    public static class DIRegistrationExtensions
    {
        public static IServiceCollection RegisterImplementations<TInterface>(this IServiceCollection serviceCollection,
            ServiceLifetime lifetime)
        {
            return serviceCollection.RegisterImplementations(typeof(TInterface), lifetime);
        }

        public static IServiceCollection RegisterImplementations(this IServiceCollection serviceCollection,
            Type interfaceType, ServiceLifetime lifetime)
        {
            var types = interfaceType.Assembly.GetTypes();
            var repos = types
            .Where(t => !t.IsAbstract && !t.IsGenericType && interfaceType.IsAssignableFrom(t)).Select(t =>
                new { Interface = t.GetInterface($"I{t.Name}"), Implementation = t });

            foreach (var repo in repos)
            {
                if (repo.Interface == null)
                    throw new ArgumentNullException(nameof(repo.Implementation),
                        $"Interface with name I{repo.Implementation.Name} for implementation {repo.Implementation.AssemblyQualifiedName} not found");

                serviceCollection.Add(repo.Interface, repo.Implementation, lifetime);
            }

            return serviceCollection;
        }

        public static IServiceCollection Add(this IServiceCollection serviceCollection, Type interfaceType,
            Type implementationType, ServiceLifetime lifetime)
        {
            serviceCollection.Add(new ServiceDescriptor(interfaceType, implementationType, lifetime));
            return serviceCollection;
        }
    }
}
