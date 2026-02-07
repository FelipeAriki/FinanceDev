using System.ComponentModel.DataAnnotations;

namespace FinanceDev.Application.DTOs;

public class TransactionCreateDTO
{
    [Required]
    public int CategoryId { get; set; }

    [Required]
    public char Type { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public string Description { get; set; } = string.Empty;
}
