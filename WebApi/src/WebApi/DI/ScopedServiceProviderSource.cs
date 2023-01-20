using System;
using Microsoft.AspNetCore.Http;

namespace WebApi.DI
{
    /// <summary>
    /// Scoped <see cref="IServiceProvider"/> source
    /// </summary>
    public class ScopedServiceProviderSource
    {
        private readonly Func<IServiceProvider> scopedServiceProviderFactory;

        /// <summary>
        /// Scoped <see cref="IServiceProvider"/> factory
        /// </summary>
        public Func<IServiceProvider> ScopedServiceProviderFactory
            => scopedServiceProviderFactory ??
               throw new NotSupportedException($"{nameof(ScopedServiceProviderSource)} has not been initialized yet");

        /// <summary>
        /// Initializes a new instance of the <see cref="ScopedServiceProviderSource"/> class
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public ScopedServiceProviderSource(IHttpContextAccessor httpContextAccessor)
        {
            scopedServiceProviderFactory = () => httpContextAccessor.HttpContext?.RequestServices;
        }
    }
}
