namespace Domain.Entities;

public class BankTransaction
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string Comment { get; set; }
    public DateTime Timestamp { get; set; }
    public string UserEmail { get; set; }

    public bool IsProcessed { get; set; } = false;

    public Guid? UserId { get; set; }
    public User? User { get; set; }
}