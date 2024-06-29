using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Restate.WebApi.Application.Abstractions;
using Restate.WebApi.Application.Abstractions.Persistence;

namespace Restate.WebApi.Application.Offers.GetOffer;

public class GetOfferQueryHandler : IRequestHandler<GetOfferQuery, GetOfferResult?>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IIdEncoder _idEncoder;
    private readonly IMapper _mapper;

    public GetOfferQueryHandler(IApplicationDbContext dbContext, IIdEncoder idEncoder, IMapper mapper)
    {
        _dbContext = dbContext;
        _idEncoder = idEncoder;
        _mapper = mapper;
    }

    public async Task<GetOfferResult?> Handle(GetOfferQuery query, CancellationToken cancellationToken)
    {
        var offerId = _idEncoder.DecodeId(query.OfferId);
        var entity = await _dbContext.Offers.AsNoTracking()
            .FirstOrDefaultAsync(offer => offer.Id == offerId, cancellationToken);
        if (entity is null)
            return null;

        var result = _mapper.Map<GetOfferResult>(entity);
        result.Id = _idEncoder.EncodeId(entity.Id);
        return result;
    }
}
