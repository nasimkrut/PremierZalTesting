using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PremierBankTesting.Bank.Models;

namespace PremierBankTesting.Bank;

public class BankApiClient
{
    public Task<List<BankTransaction>> GetRecentTransactionsAsync()
    {
        return Task.FromResult(new List<BankTransaction>
        {
            new()
            {
                Id = Guid.NewGuid(), Amount = 1000, Comment = "Пополнение", Timestamp = DateTime.UtcNow,
                UserEmail = "ivanov@test.com"
            },
            new()
            {
                Id = Guid.NewGuid(), Amount = 2000, Comment = "Оплата", Timestamp = DateTime.UtcNow,
                UserEmail = "unknown@test.com"
            },
            new()
            {
                Id = Guid.NewGuid(), Amount = -500, Comment = "Списание", Timestamp = DateTime.UtcNow,
                UserEmail = "unknown@test.com"
            },
            new()
            {
                Id = Guid.NewGuid(), Amount = 1000, Comment = "Оплата", Timestamp = DateTime.UtcNow.AddDays(-3),
                UserEmail = "ivanov@test.com"
            },
            new()
            {
                Id = Guid.NewGuid(), Amount = 1500, Comment = "Пополнение",
                Timestamp = DateTime.UtcNow.AddDays(-10), UserEmail = "petrov@test.com"
            },
            new()
            {
                Id = Guid.NewGuid(), Amount = 500, Comment = "Подарок", Timestamp = DateTime.UtcNow.AddDays(-15),
                UserEmail = "ivanov@test.com"
            }
        });
    }
}