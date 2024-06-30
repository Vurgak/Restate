var builder = DistributedApplication.CreateBuilder(args);

const string databaseConnectionName = "ApplicationDb";
const int databasePort = 65432;
var databasePassword = builder.AddParameter("postgresql-password", secret: true);

var database = builder.AddPostgres("restate-database", password: databasePassword, port: databasePort)
    .WithDataVolume()
    .AddDatabase("restate");

var grafana = builder.AddContainer("restate-grafana", "grafana/grafana")
    .WithBindMount("../Restate.Monitoring/Restate.Monitoring.Grafana/config", "/etc/grafana", isReadOnly: true)
    .WithBindMount("../Restate.Monitoring/Restate.Monitoring.Grafana/dashboards", "/var/lib/grafana/dashboards", isReadOnly: true)
    .WithHttpEndpoint(targetPort: 65433, name: "http");

builder.AddContainer("restate-prometheus", "prom/prometheus")
       .WithBindMount("../Restate.Monitoring/Restate.Monitoring.Prometheus", "/etc/prometheus", isReadOnly: true)
       .WithHttpEndpoint(/* This port is fixed as it's referenced from the Grafana config */ port: 65434, targetPort: 65434);

var webApi = builder.AddProject<Projects.Restate_WebApi>("restate-webapi")
    .WithEnvironment("GRAFANA_URL", grafana.GetEndpoint("http"))
    .WithReference(database, connectionName: databaseConnectionName);

builder.AddProject<Projects.Restate_WebApi_MigrationService>("restate-webapi-migrationservice")
    .WithReference(database, connectionName: databaseConnectionName);

builder.AddNpmApp("restate-webclient", "../Restate.WebClient")
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .WithReference(webApi);

builder.Build().Run();
