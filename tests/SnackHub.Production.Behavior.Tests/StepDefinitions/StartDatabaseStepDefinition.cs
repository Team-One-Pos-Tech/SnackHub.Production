using Npgsql;
using SnackHub.Production.Behavior.Tests.Hooks;

namespace SnackHub.Production.Behavior.Tests.StepDefinitions;

[Binding]
public class DatabaseSteps
{
    private readonly string _connectionString;

    public DatabaseSteps()
    {
        _connectionString = EnvironmentSetupHooks.GetConnectionString();
    }

    [Given(@"I have a database setup")]
    public void GivenIHaveADatabaseSetup()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
    }
}
