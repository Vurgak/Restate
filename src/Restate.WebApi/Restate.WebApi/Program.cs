using Restate.WebApi.Application.DependencyInjection;
using Restate.WebApi.DependencyInjection;
using Restate.WebApi.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddWebApi()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
builder.AddOpenTelemetry();

var application = builder.Build();
application.UseWebApi();
application.Run();
