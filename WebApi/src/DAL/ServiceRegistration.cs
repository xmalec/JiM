using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterDAL(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<DALDbContext>((options) =>
            {
                var connectionString = configuration.GetConnectionString("JiMDb");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
            return serviceCollection;
        }
    }
}
