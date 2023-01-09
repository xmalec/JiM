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
            serviceCollection.AddTransient<IUnitOfWorkGlobal, UnitOfWorkGlobal>();;
            serviceCollection.AddScoped<Func<IUnitOfWorkGlobal>>(serviceCollection => () =>
            {
                return serviceCollection.GetRequiredService<IUnitOfWorkGlobal>();
            });
            return serviceCollection;
        }

    }
}
