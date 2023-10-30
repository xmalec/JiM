using DAL.Data;
using DAL.Models;
using DAL.Repositories;
using Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUnitOfWorkGlobal, UnitOfWorkGlobal>();
            serviceCollection.AddTransient<IUnitOfWorkEventLog, UnitOfWorkEventLog>();
            serviceCollection.AddScoped<Func<IUnitOfWorkGlobal>>(serviceCollection => () =>
            {
                return serviceCollection.GetRequiredService<IUnitOfWorkGlobal>();
            });
            serviceCollection.AddScoped<Func<IUnitOfWorkEventLog>>(serviceCollection => () =>
            {
                return serviceCollection.GetRequiredService<IUnitOfWorkEventLog>();
            });
            return serviceCollection;
        }

    }
}
