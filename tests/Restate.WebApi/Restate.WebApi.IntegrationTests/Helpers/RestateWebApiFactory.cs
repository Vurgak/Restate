using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Restate.WebApi.Application.Abstractions.Persistence;
using Restate.WebApi.Infrastructure.Persistence;
using Testcontainers.PostgreSql;

namespace Restate.WebApi.IntegrationTests.Helpers;

public class RestateWebApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _databaseContainer = new PostgreSqlBuilder()
        .WithUsername("restate")
        .WithPassword("restate")
        .WithDatabase("restate")
        .WithCleanUp(true)
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<IApplicationDbContext>();
            services.RemoveAll<ApplicationDbContext>();
            services.RemoveAll<DbContextOptions<ApplicationDbContext>>();
            
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(
                options => options.UseNpgsql(_databaseContainer.GetConnectionString()),
                ServiceLifetime.Singleton);
        });
    }

    public async Task InitializeAsync()
    {
        await _databaseContainer.StartAsync();

        var dbContext = Services.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();
    }

    public new async Task DisposeAsync()
    {
        await _databaseContainer.StopAsync();
    }
}
