using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Restate.WebApi.Infrastructure.Persistence;

internal static class PropertyBuilderExtensions
{
    internal static PropertyBuilder<TEnum> HasEnumToStringConversion<TEnum>(this PropertyBuilder<TEnum> builder) where TEnum : struct, Enum
    {
        var requiredLength = Enum.GetNames<TEnum>()
            .Select(e => e.Length)
            .Max();

        builder.HasConversion<string>()
            .HasMaxLength(requiredLength);

        return builder;
    }
}
