using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AzureSearch;

public static class ServiceRegistration
{
    public const string ParentPageClient = "JobSearchClient";

    public static IServiceCollection AddImageProcessor(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        //serviceCollection.AddScoped<IParentPageSearchService, ParentPageSearchService>();
        return serviceCollection;
    }
}
