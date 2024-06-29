using MediatR;
using Restate.WebApi.Application.Offers.GetOffer;
using Restate.WebApi.Domain.Enums;

namespace Restate.WebApi.Application.Offers.GetOffers;

public record GetOffersQuery(OfferSortOrder SortOrder) : IRequest<IEnumerable<GetOfferResult>>;
