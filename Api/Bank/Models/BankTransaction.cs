using System;

namespace PremierBankTesting.Bank.Models;

public class BankTransaction
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string Comment { get; set; }
    public DateTime Timestamp { get; set; }
    public string UserEmail { get; set; }
}