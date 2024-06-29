using Microsoft.EntityFrameworkCore;
using Restate.WebApi.Application.Abstractions.Persistence;
using Restate.WebApi.Domain.Entities;

namespace Restate.WebApi.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options), IApplicationDbContext
{
    public DbSet<OfferEntity> Offers => Set<OfferEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
