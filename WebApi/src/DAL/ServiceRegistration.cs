using DAL.Data;
using DAL.Models;
using DAL.Repositories;
using Extensions.Extensions;
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
                var connectionString = configuration.GetConnectionString("app_db");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
            serviceCollection.RegisterRepositories(ServiceLifetime.Transient);
            return serviceCollection;
        }

        private static IServiceCollection RegisterRepositories(this IServiceCollection serviceCollection,
            ServiceLifetime dependencyLifetime)
        {
            serviceCollection.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            return serviceCollection.RegisterImplementations<IRepository>(dependencyLifetime);
        }
    }
}
