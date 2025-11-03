using EnterpriseWallet.IdentityService.Application.Contracts;
using EnterpriseWallet.IdentityService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseWallet.IdentityService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetSection("Connections").GetValue<string>("SqlServer");
        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseSqlServer(connectionString, sql =>
            {
                sql.MigrationsAssembly(typeof(IdentityDbContext).Assembly.FullName);
            });
        });

        services.AddScoped<IIdentityUnitOfWork, IdentityUnitOfWork>();

        return services;
    }
}
