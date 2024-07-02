using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restate.WebApi.Application.Offers.CreateOffer;
using Restate.WebApi.Application.Offers.GetOffer;
using Restate.WebApi.Application.Offers.GetOffers;

namespace Restate.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OffersController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType<ProblemDetails>(400, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(500, "application/problem+json")]
    public async Task<ActionResult<IEnumerable<GetOfferResult>>> GetOffersAsync([FromQuery] GetOffersQuery query, CancellationToken cancellationToken)
    {
        var offer = await sender.Send(query, cancellationToken);
        return Ok(offer);
    }

    [HttpGet("{OfferId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType<ProblemDetails>(400, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(500, "application/problem+json")]
    public async Task<ActionResult<GetOfferResult>> GetOfferAsync([FromRoute] GetOfferQuery query, CancellationToken cancellationToken)
    {
        var offer = await sender.Send(query, cancellationToken);
        return Ok(offer);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType<ProblemDetails>(400, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(500, "application/problem+json")]
    public async Task<ActionResult<GetOfferResult>> CreateOfferAsync([FromBody] CreateOfferCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetOfferAsync), new { OfferId = result.Id }, result);
    }
}
