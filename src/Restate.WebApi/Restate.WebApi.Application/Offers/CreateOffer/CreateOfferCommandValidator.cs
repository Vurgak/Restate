using FluentValidation;
using Restate.WebApi.Domain.Constraints;

namespace Restate.WebApi.Application.Offers.CreateOffer;

public class CreateOfferCommandValidator : AbstractValidator<CreateOfferCommand>
{
    public CreateOfferCommandValidator()
    {
        RuleFor(x => x.Title)
            .Length(OfferConstraints.MinTitleLength, OfferConstraints.MaxTitleLength);

        RuleFor(x => x.Area)
            .PrecisionScale(10, 2, ignoreTrailingZeros: true)
            .GreaterThan(0);

        RuleFor(x => x.Description)
            .NotEmpty();

        RuleFor(x => x.Price)
            .PrecisionScale(12, 2, ignoreTrailingZeros: true)
            .GreaterThan(0);
    }
}
