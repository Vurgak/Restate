using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Restate.WebApi.Infrastructure.Persistence;

namespace Restate.WebApi.MigrationService;

public class Worker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;

    public Worker(IServiceProvider serviceProvider, IHostApplicationLifetime hostApplicationLifetime)
    {
        _serviceProvider = serviceProvider;
        _hostApplicationLifetime = hostApplicationLifetime;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var serviceScope = _serviceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await WaitForDatabaseAsync(dbContext, cancellationToken);
        await EnsureDatabaseExistsAsync(dbContext, cancellationToken);
        await ApplyMigrationsAsync(dbContext, cancellationToken);

        _hostApplicationLifetime.StopApplication();
    }

    private async Task WaitForDatabaseAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        // The CanConnectAsync method always returns false in Postgres for some reason, so we need a workaround.
        await Task.Delay(2000, cancellationToken);

        //while (!await dbContext.Database.CanConnectAsync(cancellationToken))
        //{
        //    Thread.Sleep(1000);
        //}
    }

    private static async Task EnsureDatabaseExistsAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        var dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            if (!await dbCreator.ExistsAsync(cancellationToken))
            {
                await dbCreator.CreateAsync(cancellationToken);
            }
        });
    }

    private static async Task ApplyMigrationsAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            await dbContext.Database.MigrateAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });
    }
}
