using FinanceDev.Core.Models;

namespace FinanceDev.Core.Repositories;

public interface ITransactionRepository
{
    Task<IEnumerable<Transaction>> GetTransactionsAsync();
    Task<Transaction?> GetTransactionByIdAsync(int id);
    Task<int> CreateTransactionAsync(Transaction transaction);
    Task<int> UpdateTransactionAsync(Transaction transaction);
    Task<int> DeleteTransactionAsync(int id);
}
