using Dapper;
using FinanceDev.Core.Models;
using FinanceDev.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace FinanceDev.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly string _connectionString;
    public TransactionRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

    public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
    {
        using var conn = CreateConnection();
        var sql = "SELECT * FROM Transactions";
        return await conn.QueryAsync<Transaction>(sql);
    }

    public async Task<Transaction?> GetTransactionByIdAsync(int id)
    {
        using var conn = CreateConnection();
        var sql = "SELECT * FROM Transactions WHERE Id = @Id";
        return await conn.QueryFirstOrDefaultAsync<Transaction?>(sql, new { Id = id });
    }

    public async Task<int> CreateTransactionAsync(Transaction transaction)
    {
        using var conn = CreateConnection();
        var sql = "INSERT INTO Transactions(Type, CategoryId, Description, Amount, Date) VALUES(@Type, @CategoryId, @Description, @Amount, @Date)";
        return await conn.ExecuteAsync(sql, transaction);
    }

    public async Task<int> UpdateTransactionAsync(Transaction transaction)
    {
        using var conn = CreateConnection();
        var sql = "UPDATE Transactions SET Type = @Type, CategoryId = @CategoryId, Description = @Description, Amount = @Amount, Date = @Date WHER Id = @Id";
        return await conn.ExecuteAsync(sql, transaction);
    }

    public async Task<int> DeleteTransactionAsync(int id)
    {
        using var conn = CreateConnection();
        var sql = "DELETE FROM Transactions WHERE Id = @Id";
        return await conn.ExecuteAsync(sql, new { Id = id });
    }
}
