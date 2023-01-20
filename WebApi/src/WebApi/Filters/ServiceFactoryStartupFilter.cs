using WebApi.DI;

namespace WebApi.Filters
{
    /// <summary>
    /// Sets up <see cref="ServiceFactory"/>
    /// </summary>
    public class ServiceFactoryStartupFilter : IStartupFilter
    {
        /// <summary>
        /// Sets up <see cref="ServiceFactory"/>
        /// </summary>
        /// <param name="next"></param>
        /// <returns></returns>
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                var scopedServiceProviderSource =
                    builder.ApplicationServices.GetRequiredService<ScopedServiceProviderSource>();
                ServiceFactory.Current =
                    new WebServiceFactory(scopedServiceProviderSource.ScopedServiceProviderFactory);
                next(builder);
            };
        }
    }
}
