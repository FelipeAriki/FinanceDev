using FinanceDev.Application.DTOs;
using FinanceDev.Application.ViewModels;
using FinanceDev.Core.Models;
using FinanceDev.Core.Repositories;

namespace FinanceDev.Application.Services;

public class FinanceService : IFinanceService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICategoryRepository _categoryRepository;
    public FinanceService(ITransactionRepository transactionRepository, ICategoryRepository categoryRepository)
    {
        _transactionRepository = transactionRepository;
        _categoryRepository = categoryRepository;
    }
    public async Task CreateTransactionAsync(TransactionCreateDTO t)
    {
        if (t.Amount <= 0) throw new ArgumentException("A quantidade precisa ser superior a zero", nameof(t.Amount));
        if (t.Type != 'R' && t.Type != 'D') throw new ArgumentException("O tipo precisa ser R (Receita) ou D (Despesa)", nameof(t.Type));

        await _transactionRepository.CreateTransactionAsync(new Transaction
        {
            Id = t.Id,
            CategoryId = t.CategoryId,
            Date = t.Date,
            Amount = t.Amount,
            Type = t.Type,
            Description = t.Description
        });
    }

    public async Task DeleteTransactionAsync(int id)
    {
        await _transactionRepository.DeleteTransactionAsync(id);
    }

    public async Task<DashboardDTO> GetDashboardAsync(int month, int year)
    {
        if (month < 1 || month > 9999)
            throw new ArgumentException("Mês inválido", nameof(month));
        if (year < 1 || year > 9999)
            throw new ArgumentException("Ano inválido", nameof(year));


        var all = await _transactionRepository.GetTransactionsAsync();

        var atual = all.Where(t => t.Date.Month == month && t.Date.Year == year);

        int prevMonth = month == 1 ? 12 : month - 1;
        int prevYear = month == 1 ? year - 1 : year;

        IEnumerable<Transaction> anterior = Enumerable.Empty<Transaction>();

        if (prevYear >= 1)
            anterior = all.Where(t => t.Date.Month == prevMonth && t.Date.Year == prevYear);


        decimal receitas = atual.Where(t => t.Type == 'R').Sum(t => t.Amount);
        decimal despesas = atual.Where(t => t.Type == 'D').Sum(t => t.Amount);
        decimal saldo = receitas - despesas;

        decimal saldoAnterior = anterior.Any() ?
            anterior.Sum(t => t.Type == 'R' ? t.Amount : -t.Amount) : 0m;

        decimal diferenca = saldo - saldoAnterior;
        decimal percentual = saldoAnterior != 0 ?
            Math.Round((diferenca / Math.Abs(saldoAnterior)) * 100, 2) : (diferenca == 0 ? 0 : 100);

        return new DashboardDTO
        {
            Revenue = receitas,
            Expenses = despesas,
            Balance = saldo,
            PreviousBalance = saldoAnterior,
            Difference = diferenca,
            GrowthPercentage = percentual
        };
    }

    public async Task<IEnumerable<CategoryTotalDTO>> GetTotalsByCategoryAsync(int? month = null, int? year = null)
    {
        var all = await _transactionRepository.GetTransactionsAsync();
        var categories = await _categoryRepository.GetCategoriesAsync();

        if (year.HasValue)
            all = all.Where(t => t.Date.Year == year.Value);

        if (month.HasValue)
            all = all.Where(t => t.Date.Month == month.Value);

        var grouped = all
            .GroupBy(t => t.CategoryId)
            .Select(g =>
            {
                var category = categories.FirstOrDefault(c => c.Id == g.Key);

                return new CategoryTotalDTO
                {
                    CategoryId = g.Key,
                    CategoryName = category?.Name ?? "Categoria Desconhecida",
                    Total = g.Sum(t => t.Amount)

                };

            }).ToList();

        return grouped;
    }

    public async Task<TransactionViewModel?> GetTransactionByIdAsync(int id)
    {
        var t = await _transactionRepository.GetTransactionByIdAsync(id);
        return new TransactionViewModel
        {
            Id = t.Id,
            CategoryId = t.CategoryId,
            Date = t.Date,
            Amount = t.Amount,
            Type = t.Type,
            Description = t.Description
        };
    }

    public async Task<IEnumerable<TransactionViewModel>> GetTransactionsAsync(int? month = null, int? year = null, char? type = null)
    {
        var all = await _transactionRepository.GetTransactionsAsync();
        if (month.HasValue)
            all = all.Where(t => t.Date.Month == month.Value);
        if (year.HasValue)
            all = all.Where(t => t.Date.Year == year.Value);
        if (type.HasValue)
            all = all.Where(t => t.Type == type.Value);
        return all
        .OrderByDescending(t => t.Date)
        .Select(t => new TransactionViewModel
        {
            Id = t.Id,
            CategoryId = t.CategoryId,
            Date = t.Date,
            Amount = t.Amount,
            Type = t.Type,
            Description = t.Description
        });
    }

    public async Task UpdateTransactionAsync(TransactionCreateDTO t)
    {
        if (t.Amount <= 0) throw new ArgumentException("A quantidade precisa ser superior a zero", nameof(t.Amount));
        if (t.Type != 'R' && t.Type != 'D') throw new ArgumentException("O tipo precisa ser R (Receita) ou D (Despesa)", nameof(t.Type));

        await _transactionRepository.UpdateTransactionAsync(new Transaction
        {
            Id = t.Id,
            CategoryId = t.CategoryId,
            Date = t.Date,
            Amount = t.Amount,
            Type = t.Type,
            Description = t.Description
        });
    }
}
