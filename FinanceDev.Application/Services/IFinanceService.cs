using FinanceDev.Application.DTOs;
using FinanceDev.Application.ViewModels;

namespace FinanceDev.Application.Services;

public interface IFinanceService
{
    Task<IEnumerable<TransactionViewModel>> GetTransactionsAsync(int? month = null, int? year = null, char? type = null);
    Task<TransactionViewModel?> GetTransactionByIdAsync(int id);
    Task CreateTransactionAsync(TransactionCreateDTO transactionCreateDTO);
    Task UpdateTransactionAsync(TransactionCreateDTO transactionCreateDTO);
    Task DeleteTransactionAsync(int id);

    Task<DashboardDTO> GetDashboardAsync(int month, int year);
    Task<IEnumerable<CategoryTotalDTO>> GetTotalsByCategoryAsync(int? month = null, int? year = null);
}
