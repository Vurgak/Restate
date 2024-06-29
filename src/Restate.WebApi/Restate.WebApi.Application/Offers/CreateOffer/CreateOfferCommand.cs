using MediatR;
using Restate.WebApi.Domain.Enums;

namespace Restate.WebApi.Application.Offers.CreateOffer;

public class CreateOfferCommand : IRequest<string?>
{
    public string Title { get; set; } = string.Empty;

    public EstateKind EstateKind { get; set; }

    public decimal Area { get; set; }

    public string Description { get; set; } = string.Empty;

    public decimal? Price { get; set; }
}
