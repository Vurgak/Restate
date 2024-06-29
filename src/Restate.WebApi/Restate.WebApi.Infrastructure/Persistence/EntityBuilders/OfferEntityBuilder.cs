using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restate.WebApi.Domain.Constraints;
using Restate.WebApi.Domain.Entities;

namespace Restate.WebApi.Infrastructure.Persistence.EntityBuilders;

public class OfferEntityBuilder : IEntityTypeConfiguration<OfferEntity>
{
    public void Configure(EntityTypeBuilder<OfferEntity> builder)
    {
        builder.Property(e => e.Title)
            .HasMaxLength(OfferConstraints.MaxTitleLength);

        builder.Property(e => e.EstateKind)
            .HasEnumToStringConversion();

        builder.Property(e => e.Status)
            .HasEnumToStringConversion();
    }
}
