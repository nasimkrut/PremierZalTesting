using System;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure;
using Infrastructure.BankApiClient;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class TransactionImportService : ITransactionImportService
{
    private readonly IBankApiClient _bankApiClient;
    private readonly ApplicationDbContext _dbContext;

    public TransactionImportService(IBankApiClient bankApiClient, ApplicationDbContext dbContext)
    {
        _bankApiClient = bankApiClient;
        _dbContext = dbContext;
    }

    public async Task ImportTransactionsAsync()
    {
        var bankTransactions = await _bankApiClient.GetRecentTransactionsAsync();

        foreach (var transaction in bankTransactions)
        {
            if (await _dbContext.Transactions.AnyAsync(t => t.Id == transaction.Id))
                continue;

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == transaction.UserEmail);

            if (user == null)
            {
                user = new User { Email = transaction.UserEmail };
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
            }

            var newTransaction = new BankTransaction
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Comment = transaction.Comment,
                IsProcessed = false,
                Timestamp = transaction.Timestamp,
                UserEmail = transaction.UserEmail,
                UserId = user.Id,
                
            };
            
            _dbContext.Transactions.Add(newTransaction);
        }

        await _dbContext.SaveChangesAsync();
    }

    public Task GetUnProcessedTransactionsAsync()
    {
        throw new NotImplementedException();
    }
}