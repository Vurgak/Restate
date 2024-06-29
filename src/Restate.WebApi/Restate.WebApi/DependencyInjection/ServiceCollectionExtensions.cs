using FluentValidation.AspNetCore;
using System.Text.Json.Serialization;

namespace Restate.WebApi.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebApi(this IServiceCollection services)
    {
        services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false)
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        services.AddSwaggerGen();
        services.AddFluentValidationAutoValidation();
        return services;
    }
}
