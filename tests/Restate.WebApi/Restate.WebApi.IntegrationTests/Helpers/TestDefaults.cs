using System.Text.Json.Serialization;
using System.Text.Json;

namespace Restate.WebApi.IntegrationTests.Helpers;

internal static class TestDefaults
{
    public static readonly JsonSerializerOptions JsonOptions = new()
    {
        Converters = { new JsonStringEnumConverter() },
        PropertyNameCaseInsensitive = true,
    };
}
