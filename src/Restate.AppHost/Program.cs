var builder = DistributedApplication.CreateBuilder(args);

const string databaseConnectionName = "ApplicationDb";
const int databasePort = 65432;
var databasePassword = builder.AddParameter("postgresql-password", secret: true);

var database = builder.AddPostgres("restate-database", password: databasePassword, port: databasePort)
    .WithDataVolume()
    .AddDatabase("restate");

var webApi = builder.AddProject<Projects.Restate_WebApi>("restate-webapi")
    .WithReference(database, connectionName: databaseConnectionName);

builder.AddProject<Projects.Restate_WebApi_MigrationService>("restate-webapi-migrationservice")
    .WithReference(database, connectionName: databaseConnectionName);

builder.AddNpmApp("restate-webclient", "../Restate.WebClient")
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .WithReference(webApi);

builder.Build().Run();
