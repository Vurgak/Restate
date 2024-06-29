using AutoMapper;
using Restate.WebApi.Application.Abstractions;
using Restate.WebApi.Application.Offers.CreateOffer;
using Restate.WebApi.Application.Offers.GetOffer;
using Restate.WebApi.Domain.Entities;
using Restate.WebApi.Domain.Enums;

namespace Restate.WebApi.Application.Offers;

internal class OfferMappings : Profile
{
    public OfferMappings()
    {
        CreateMap<CreateOfferCommand, OfferEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedOn, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => OfferStatus.Unpublished));

        CreateMap<OfferEntity, GetOfferResult>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
