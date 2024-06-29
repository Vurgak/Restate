using Microsoft.EntityFrameworkCore;
using Restate.WebApi.Infrastructure.Persistence;
using Restate.WebApi.MigrationService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ApplicationDb")));

var host = builder.Build();
host.Run();
