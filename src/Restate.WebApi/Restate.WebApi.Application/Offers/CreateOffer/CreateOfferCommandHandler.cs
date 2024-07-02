using AutoMapper;
using MediatR;
using Restate.WebApi.Application.Abstractions;
using Restate.WebApi.Application.Abstractions.Persistence;
using Restate.WebApi.Application.Offers.GetOffer;
using Restate.WebApi.Domain.Entities;

namespace Restate.WebApi.Application.Offers.CreateOffer;

internal class CreateOfferCommandHandler : IRequestHandler<CreateOfferCommand, GetOfferResult>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IIdEncoder _idEncoder;
    private readonly IMapper _mapper;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateOfferCommandHandler(
        IApplicationDbContext dbContext,
        IIdEncoder idEncoder,
        IMapper mapper,
        IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext;
        _idEncoder = idEncoder;
        _mapper = mapper;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<GetOfferResult> Handle(CreateOfferCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<OfferEntity>(command);
        entity.CreatedOn = _dateTimeProvider.UtcNow;

        await _dbContext.Offers.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<GetOfferResult>(entity);
        result.Id = _idEncoder.EncodeId(entity.Id);
        return result;
    }
}
