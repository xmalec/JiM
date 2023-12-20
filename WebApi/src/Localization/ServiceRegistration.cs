using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace WebApi
{
    public static class ServiceRegistration
    {
        
        public static IServiceCollection RegisterLocalization(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddLocalization(options => options.ResourcesPath = "Resources");
    //        serviceCollection.Configure<RequestLocalizationOptions>(options => {
    //            var supportedCultures = new List<CultureInfo> {
    //    new CultureInfo("cs-CZ"),
    //    new CultureInfo("en-US")
    //};
    //            options.DefaultRequestCulture = new RequestCulture(culture: "fr-FR", uiCulture: "fr-FR");
    //            options.SupportedCultures = supportedCultures;
    //            options.SupportedUICultures = supportedCultures;
    //            options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
    //        });
            return serviceCollection;
        }
    }
}
