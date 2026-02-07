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

    public Task<int> CreateTransactionAsync(Transaction transaction)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateTransactionAsync(Transaction transaction)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteTransactionAsync(int id)
    {
        using var conn = CreateConnection();
        var sql = "DELETE FROM Transactions WHERE Id = @Id";
        return conn.ExecuteAsync(sql, new { Id = id });
    }
}
