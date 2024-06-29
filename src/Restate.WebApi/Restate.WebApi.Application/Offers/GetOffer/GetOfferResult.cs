using Restate.WebApi.Domain.Enums;

namespace Restate.WebApi.Application.Offers.GetOffer;

public class GetOfferResult
{
    public string Id { get; set; } = string.Empty;

    public DateTimeOffset CreatedOn { get; set; }

    public DateTimeOffset? ModifiedOn { get; set; }

    public string Title { get; set; } = string.Empty;

    public EstateKind EstateKind { get; set; }

    public string Description { get; set; } = string.Empty;

    public decimal? Price { get; set; }

    public OfferStatus Status { get; set; }
}
