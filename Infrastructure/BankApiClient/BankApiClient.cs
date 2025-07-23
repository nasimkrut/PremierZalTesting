using System.Security.Cryptography;
using System.Text;
using Domain.Entities;

namespace Infrastructure.BankApiClient;

public class BankApiClient : IBankApiClient
{
    public Task<List<Transaction>> GetRecentTransactionsAsync()
    {
        return Task.FromResult(new List<Transaction>
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
            // new()
            // {
            //     Id = GenerateUserGuid("ivanov@test.com" + DateTime.UtcNow), Amount = 1000, Comment = "Пополнение", Timestamp = DateTime.UtcNow,
            //     UserEmail = "ivanov@test.com"
            // },
            // new()
            // {
            //     Id = GenerateUserGuid("unknown@test.com" + DateTime.UtcNow), Amount = 2000, Comment = "Оплата", Timestamp = DateTime.UtcNow,
            //     UserEmail = "unknown@test.com"
            // },
            // new()
            // {
            //     Id = GenerateUserGuid("unknown@test.com" + DateTime.UtcNow), Amount = -500, Comment = "Списание", Timestamp = DateTime.UtcNow,
            //     UserEmail = "unknown@test.com"
            // },
            // new()
            // {
            //     Id = GenerateUserGuid("unknown@test.com" + DateTime.UtcNow), Amount = 1000, Comment = "Оплата", Timestamp = DateTime.UtcNow.AddDays(-3),
            //     UserEmail = "unknown@test.com"
            // },
            // new()
            // {
            //     Id = GenerateUserGuid("petrov@test.com" + DateTime.UtcNow), Amount = 1500, Comment = "Пополнение",
            //     Timestamp = DateTime.UtcNow.AddDays(-10), UserEmail = "petrov@test.com"
            // },
            // new()
            // {
            //     Id = GenerateUserGuid("ivanov@test.com" + DateTime.UtcNow), Amount = 500, Comment = "Подарок", Timestamp = DateTime.UtcNow.AddDays(-15),
            //     UserEmail = "ivanov@test.com"
            // }

        });
    }
    
    private static Guid GenerateDeterministicGuid(string input)
    {
        var inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes;
        using (var sha256 = SHA256.Create())
            hashBytes = sha256.ComputeHash(inputBytes);
        
        var guidBytes = new byte[16];
        Array.Copy(hashBytes, guidBytes, 16);

        return new Guid(guidBytes);
    }
}