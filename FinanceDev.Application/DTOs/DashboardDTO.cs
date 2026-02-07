namespace FinanceDev.Application.DTOs;

public class DashboardDTO
{
    public decimal Revenue { get; set; }
    public decimal Expenses { get; set; }
    public decimal Balance { get; set; }
    public decimal PreviousBalance { get; set; }
    public decimal Difference { get; set; }
    public decimal GrowthPercentage { get; set; }
}
