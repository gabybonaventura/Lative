namespace Lative.Infrastructure;

using Npgsql;

public class TableInitializer
{
    private readonly string _connectionString;

    public TableInitializer(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void InitializeTables()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using var command = new NpgsqlCommand();
        command.Connection = connection;

        // Create Sales table
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Sales (
                Id SERIAL PRIMARY KEY,
                OpportunityId TEXT NOT NULL,
                OpportunityOwner TEXT NOT NULL,
                Currency TEXT NOT NULL,
                Amount FLOAT NOT NULL,
                CloseDate DATE NOT NULL
            );";
        command.ExecuteNonQuery();

        // Create Dimensions table
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Dimensions (
                Id SERIAL PRIMARY KEY,
                SaleId INTEGER REFERENCES Sales (Id) ON DELETE CASCADE,
                Name TEXT NOT NULL,
                Value TEXT NOT NULL,
                StartDate DATE,
                EndDate DATE
            );";
        command.ExecuteNonQuery();
    }
}
