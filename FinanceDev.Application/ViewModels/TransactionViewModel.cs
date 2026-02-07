namespace FinanceDev.Application.ViewModels;

public class TransactionViewModel
{
    public int Id { get; set; }
    public int CategoryId { get; set; }

    public char Type { get; set; }

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }

    public string Description { get; set; } = string.Empty;
}
