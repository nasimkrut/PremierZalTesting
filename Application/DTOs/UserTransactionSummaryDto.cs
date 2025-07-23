namespace Application.DTOs;

public class UserTransactionSummaryDto
{
    public string UserEmail { get; set; } = default!;
    public decimal TotalAmount { get; set; }
}