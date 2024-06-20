var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("restate-database");
var database = postgres.AddDatabase("restate");

var webApi = builder.AddProject<Projects.Restate_WebApi>("restate-webapi")
    .WithReference(database, connectionName: "ApplicationDb");

builder.AddNpmApp("restate-webclient", "../Restate.WebClient")
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .WithReference(webApi)
    .PublishAsDockerFile();

builder.Build().Run();
