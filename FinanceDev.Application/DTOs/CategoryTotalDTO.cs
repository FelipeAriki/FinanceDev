namespace FinanceDev.Application.DTOs;

public class CategoryTotalDTO
{
    public int CategoryId { get; set; }
    public decimal Total { get; set; }
    public string CategoryName { get; set; } = string.Empty;
}
