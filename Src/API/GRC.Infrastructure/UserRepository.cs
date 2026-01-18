using Dapper;
using GRC.Domain.Entities;
using GRC.Domain.Interfaces;

namespace GRC.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SqliteConnectionFactory _connectionFactory;

    public UserRepository(SqliteConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT * FROM Users WHERE Email = @Email";
        return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT * FROM Users WHERE Id = @Id";
        return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT * FROM Users";
        return await connection.QueryAsync<User>(sql);
    }

    public async Task<int> AddAsync(User user)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"INSERT INTO Users (Email, PasswordHash, OrganizationId, RoleId, Active, CreatedAt, MobileNumber)
                    VALUES (@Email, @PasswordHash, @OrganizationId, @RoleId, @Active, @CreatedAt, @MobileNumber);
                    SELECT last_insert_rowid();";
        return await connection.ExecuteScalarAsync<int>(sql, user);
    }

    public async Task UpdateAsync(User user)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"UPDATE Users SET Email = @Email, PasswordHash = @PasswordHash,
                    OrganizationId = @OrganizationId, RoleId = @RoleId, Active = @Active, MobileNumber = @MobileNumber
                    WHERE Id = @Id";
        await connection.ExecuteAsync(sql, user);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "DELETE FROM Users WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id });
    }

    public async Task AssignRoleAsync(int userId, int roleId)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "UPDATE Users SET RoleId = @RoleId WHERE Id = @UserId";
        await connection.ExecuteAsync(sql, new { UserId = userId, RoleId = roleId });
    }
}