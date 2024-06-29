using Restate.WebApi.Domain.Enums;

namespace Restate.WebApi.Domain.Entities;

public class OfferEntity
{
    public int Id { get; set; }

    public DateTimeOffset CreatedOn { get; set; }

    public DateTimeOffset? ModifiedOn { get; set; }

    public string Title { get; set; } = string.Empty;

    public EstateKind EstateKind { get; set; }

    public string Description { get; set; } = string.Empty;

    public decimal? Price {  get; set; }

    public OfferStatus Status { get; set; }
}
