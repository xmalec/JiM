using System;
using Microsoft.Extensions.Options;

namespace BL.DI
{
    public interface IServiceFactory
    {
        IServiceProvider ServiceProvider { get; }
        IOptionsMonitor<T> GetOptions<T>();
        T GetService<T>();
    }
}
