using Restate.WebApi.Application.Abstractions;

namespace Restate.WebApi.Infrastructure;

internal class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
