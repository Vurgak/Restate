namespace Restate.WebApi.Application.Abstractions;

public interface IDateTimeProvider
{
    public DateTimeOffset UtcNow { get; }
}
