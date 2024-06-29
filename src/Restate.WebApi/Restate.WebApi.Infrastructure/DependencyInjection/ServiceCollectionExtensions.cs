using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restate.WebApi.Application.Abstractions;
using Restate.WebApi.Application.Abstractions.Persistence;
using Restate.WebApi.Infrastructure.Persistence;

namespace Restate.WebApi.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IIdEncoder, HashIdEncoder>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("ApplicationDb")));

        return services;
    }
}
