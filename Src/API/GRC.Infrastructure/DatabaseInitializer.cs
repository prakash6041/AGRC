using Microsoft.Data.Sqlite;
using System.IO;

namespace GRC.Infrastructure;

public static class DatabaseInitializer
{
    public static void Initialize(string connectionString)
    {
        // Extract database path from connection string
        var dataSource = connectionString.Replace("Data Source=", "").Trim();
        var directory = Path.GetDirectoryName(dataSource);

        // Create directory if it doesn't exist
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Create database file if it doesn't exist
        if (!File.Exists(dataSource))
        {
            // Create the file by opening and closing a connection
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            connection.Close();
        }

        // Run schema.sql to create tables
        using var conn = new SqliteConnection(connectionString);
        conn.Open();

        var schemaPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "schema.sql");
        if (File.Exists(schemaPath))
        {
            var schemaSql = File.ReadAllText(schemaPath);
            using var command = conn.CreateCommand();
            command.CommandText = schemaSql;
            command.ExecuteNonQuery();
        }
        else
        {
            // Fallback to hardcoded if file not foclsund
            CreateTablesFallback(conn);
        }
    }

    private static void CreateTablesFallback(SqliteConnection connection)
    {
        var createUsersTable = @"
            CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Email TEXT NOT NULL UNIQUE,
                PasswordHash TEXT NOT NULL,
                FirstName TEXT NOT NULL,
                LastName TEXT NOT NULL,
                OrganizationId INTEGER NOT NULL,
                RoleId INTEGER NOT NULL,
                IsActive INTEGER NOT NULL DEFAULT 1,
                CreatedAt TEXT NOT NULL,
                OtpCode TEXT,
                OtpExpiry TEXT
            );";

        var createOrganizationsTable = @"
            CREATE TABLE IF NOT EXISTS Organizations (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Description TEXT,
                IsActive INTEGER NOT NULL DEFAULT 1,
                CreatedAt TEXT NOT NULL
            );";

        var createRolesTable = @"
            CREATE TABLE IF NOT EXISTS Roles (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Description TEXT
            );";

        using var command = connection.CreateCommand();
        command.CommandText = createUsersTable;
        command.ExecuteNonQuery();

        command.CommandText = createOrganizationsTable;
        command.ExecuteNonQuery();

        command.CommandText = createRolesTable;
        command.ExecuteNonQuery();

        // Insert default role
        command.CommandText = "INSERT OR IGNORE INTO Roles (Id, Name, Description) VALUES (1, 'User', 'Default user role')";
        command.ExecuteNonQuery();
    }
}