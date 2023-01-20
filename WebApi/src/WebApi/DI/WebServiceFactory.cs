using Microsoft.Extensions.Options;

namespace WebApi.DI
{
    public class WebServiceFactory : IServiceFactory
    {
        private readonly Func<IServiceProvider> serviceProviderFactory;

        public WebServiceFactory(Func<IServiceProvider> serviceProviderFactory)
        {
            this.serviceProviderFactory = serviceProviderFactory;
        }

        public IServiceProvider ServiceProvider => serviceProviderFactory();
        public IOptionsMonitor<T> GetOptions<T>()
        {
            return ServiceProvider.GetRequiredService<IOptionsMonitor<T>>();
        }

        public T GetService<T>()
        {
            return ServiceProvider.GetRequiredService<T>();
        }
    }
}
