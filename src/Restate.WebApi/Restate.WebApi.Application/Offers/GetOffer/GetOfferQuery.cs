using MediatR;

namespace Restate.WebApi.Application.Offers.GetOffer;

public record GetOfferQuery(string OfferId) : IRequest<GetOfferResult?>;
