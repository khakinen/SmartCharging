using Data.Implementation.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartCharging.Data.Contract;
using SmartCharging.Data.Contract.Repositories;

namespace Data.Implementation;

public static class DependencyInjection
{
    public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContext<SmartChargingDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("SmartCharging") ??
                                                throw new InvalidOperationException("DB ConnectionString is missing")))

            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IGroupRepository, GroupRepository>()
            .AddScoped<IChargeStationRepository, ChargeStationRepository>()
            .AddScoped<IConnectorRepository, ConnectorRepository>();
    }
}