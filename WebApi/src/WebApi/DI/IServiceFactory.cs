using System;
using Microsoft.Extensions.Options;

namespace WebApi.DI
{
    public interface IServiceFactory
    {
        IServiceProvider ServiceProvider { get; }
        IOptionsMonitor<T> GetOptions<T>();
        T GetService<T>();
    }
}
