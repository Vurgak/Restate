using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Restate.WebApi.Application.Abstractions;
using Restate.WebApi.Application.Abstractions.Persistence;
using Restate.WebApi.Application.Offers.GetOffer;
using Restate.WebApi.Domain.Enums;

namespace Restate.WebApi.Application.Offers.GetOffers;

public class GetOffersQueryHandler : IRequestHandler<GetOffersQuery, IEnumerable<GetOfferResult>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IIdEncoder _idEncoder;

    public GetOffersQueryHandler(IApplicationDbContext dbContext, IMapper mapper, IIdEncoder idEncoder)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _idEncoder = idEncoder;
    }

    public async Task<IEnumerable<GetOfferResult>> Handle(GetOffersQuery query, CancellationToken cancellationToken)
    {
        var entitiesQuery = _dbContext.Offers.AsNoTracking();
        entitiesQuery = query.SortOrder switch
        {
            OfferSortOrder.CreatedAscending => entitiesQuery.OrderBy(e => e.CreatedOn),
            OfferSortOrder.CreatedDescending => entitiesQuery.OrderByDescending(e => e.CreatedOn),
            OfferSortOrder.ModifiedAscending => entitiesQuery.OrderBy(e => e.ModifiedOn ?? e.CreatedOn),
            OfferSortOrder.ModifiedDescending => entitiesQuery.OrderByDescending(e => e.ModifiedOn ?? e.CreatedOn),
            OfferSortOrder.PriceAscending => entitiesQuery.OrderBy(e => e.Price),
            OfferSortOrder.PriceDescending => entitiesQuery.OrderByDescending(e => e.Price),
            _ => throw new InvalidOperationException(),
        };
        var entities = await entitiesQuery.ToListAsync(cancellationToken);

        var results = _mapper.Map<IEnumerable<GetOfferResult>>(entitiesQuery);
        return results;
    }
}
