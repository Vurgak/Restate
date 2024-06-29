using Microsoft.EntityFrameworkCore;
using Restate.WebApi.Domain.Entities;

namespace Restate.WebApi.Application.Abstractions.Persistence;

public interface IApplicationDbContext
{
    public DbSet<OfferEntity> Offers { get; }

    Task<int> SaveChangesAsync(CancellationToken ct);
}
