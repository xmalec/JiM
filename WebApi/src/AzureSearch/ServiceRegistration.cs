using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
using Azure;
using AzureSearch.Services.JobRequisition;

namespace AzureSearch;

public static class ServiceRegistration
{
    public static IServiceCollection AddSearch(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddAzureClientsCore();

        var searchClientOptions = new SearchClientOptions();
        configuration.GetSection("AzureSearchClient").Bind(searchClientOptions);

        serviceCollection.AddAzureClients(builder =>
        {
            builder
            .AddSearchClient(searchClientOptions.Endpoint, searchClientOptions.IndexNameJobRequisition, new AzureKeyCredential(searchClientOptions.CredentialKey))
            .WithName("JobSearchClient");
            builder.AddSearchIndexClient(searchClientOptions.Endpoint, new AzureKeyCredential(searchClientOptions.CredentialKey));
        });
        serviceCollection.AddScoped<IJobRequisitionSearchService, JobRequisitionSearchService>();
        return serviceCollection;
    }
}
