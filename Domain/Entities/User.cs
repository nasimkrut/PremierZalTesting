namespace Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public List<BankTransaction> Transactions { get; set; } = new();
}