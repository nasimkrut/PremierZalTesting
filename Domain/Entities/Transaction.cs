namespace Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string Comment { get; set; }
    public DateTime Timestamp { get; set; }
    public string UserEmail { get; set; }

    public bool IsProcessed { get; set; }

    public Guid? UserId { get; set; }
    public User? User { get; set; }
}