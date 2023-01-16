using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
using Azure;
using AzureSearch.Services.ParentPage;

namespace AzureSearch;

public static class ServiceRegistration
{
    public const string ParentPageClient = "JobSearchClient";

    public static IServiceCollection AddAzureSearch(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddAzureClientsCore();

        var searchClientOptions = new SearchClientOptions();
        configuration.GetSection(SearchClientOptions.SectionName).Bind(searchClientOptions);

        serviceCollection.AddAzureClients(builder =>
        {
            builder
            .AddSearchClient(searchClientOptions.Endpoint, searchClientOptions.IndexNameJobRequisition, new AzureKeyCredential(searchClientOptions.CredentialKey))
            .WithName(ParentPageClient);
            builder.AddSearchIndexClient(searchClientOptions.Endpoint, new AzureKeyCredential(searchClientOptions.CredentialKey));
        });
        serviceCollection.AddScoped<IParentPageSearchService, ParentPageSearchService>();
        return serviceCollection;
    }
}
