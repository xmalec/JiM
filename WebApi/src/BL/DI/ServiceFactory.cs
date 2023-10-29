using Microsoft.Extensions.Options;

namespace BL.DI
{
    public class ServiceFactory : IServiceFactory
    {
        public static IServiceFactory Current { get; set; } = new ServiceFactory();
        public IServiceProvider ServiceProvider => throw new NotImplementedException();
        public IOptionsMonitor<T> GetOptions<T>()
        {
            throw new NotImplementedException();
        }

        public T GetService<T>() => throw new NotImplementedException();
    }
}
