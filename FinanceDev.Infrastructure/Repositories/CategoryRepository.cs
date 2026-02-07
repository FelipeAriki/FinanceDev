using Dapper;
using FinanceDev.Core.Models;
using FinanceDev.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace FinanceDev.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly string _connectionString;
    public CategoryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        using var conn = CreateConnection();
        var sql = "SELECT * FROM Categories";
        return await conn.QueryAsync<Category>(sql);
    }

    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        using var conn = CreateConnection();
        var sql = "SELECT * FROM Categories WHERE Id = @Id";
        return await conn.QueryFirstOrDefaultAsync<Category?>(sql, new { Id = id});
    }
}
