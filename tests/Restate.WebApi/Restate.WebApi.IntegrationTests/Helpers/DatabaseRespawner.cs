using Respawn;
using System.Data.Common;

namespace Restate.WebApi.IntegrationTests.Helpers;

public class DatabaseRespawner
{
    private readonly DbConnection _dbConnection;
    private Respawner _respawner = null!;

    public DatabaseRespawner(DbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task InitializeAsync()
    {
        _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["public"],
        });
    }

    public async Task ResetAsync()
    {
        await _dbConnection.OpenAsync();
        await _respawner.ResetAsync(_dbConnection);
        await _dbConnection.CloseAsync();
    }
}
