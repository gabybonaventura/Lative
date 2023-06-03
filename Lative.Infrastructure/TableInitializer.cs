namespace Lative.Infrastructure;
using System;

using Npgsql;

public class TableInitializer : ITableInitializer
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
            OpportunityId TEXT PRIMARY KEY NOT NULL,
            OpportunityOwner TEXT NOT NULL,
            Currency TEXT NOT NULL,
            Amount NUMERIC NOT NULL,
            CloseDate DATE NOT NULL
        );";
        command.ExecuteNonQuery();

        // Create Dimensions table
        command.CommandText = @"
        CREATE TABLE IF NOT EXISTS Dimensions (
            Id SERIAL PRIMARY KEY,
            Name TEXT,
            Value TEXT,
            StartDate DATE NULL,
            EndDate DATE NULL,
            SaleOperationModelId TEXT,
            FOREIGN KEY (SaleOperationModelId) REFERENCES Sales (OpportunityId)
        );";
        command.ExecuteNonQuery();
    }

}
