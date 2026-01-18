using Dapper;
using GRC.Domain.Entities;
using GRC.Domain.Interfaces;

namespace GRC.Infrastructure.Repositories;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly SqliteConnectionFactory _connectionFactory;

    public OrganizationRepository(SqliteConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Organization?> GetByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT * FROM Organizations WHERE Id = @Id";
        return await connection.QueryFirstOrDefaultAsync<Organization>(sql, new { Id = id });
    }

    public async Task<IEnumerable<Organization>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT * FROM Organizations";
        return await connection.QueryAsync<Organization>(sql);
    }

    public async Task<int> AddAsync(Organization organization)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"INSERT INTO Organizations (Name, Description, IsActive, CreatedAt)
                    VALUES (@Name, @Description, @IsActive, @CreatedAt);
                    SELECT last_insert_rowid();";
        return await connection.ExecuteScalarAsync<int>(sql, organization);
    }

    public async Task UpdateAsync(Organization organization)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"UPDATE Organizations SET Name = @Name, Description = @Description, IsActive = @IsActive
                    WHERE Id = @Id";
        await connection.ExecuteAsync(sql, organization);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "DELETE FROM Organizations WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id });
    }
}