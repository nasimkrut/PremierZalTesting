using System;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure;
using Infrastructure.BankApiClient;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class TransactionService : ITransactionService
{
    private readonly IBankApiClient _bankApiClient;
    private readonly ApplicationDbContext _dbContext;

    public TransactionService(IBankApiClient bankApiClient, ApplicationDbContext dbContext)
    {
        _bankApiClient = bankApiClient;
        _dbContext = dbContext;
    }

    #region Import
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
    
    #endregion

    #region QueryServices

    public async Task<List<BankTransactionDto>> GetUnprocessedTransactionsAsync()
    {
        return await _dbContext.Transactions
            .Where(t => !t.IsProcessed)
            .Select(t => new BankTransactionDto
            {
                Id = t.Id,
                Amount = t.Amount,
                Comment = t.Comment,
                Timestamp = t.Timestamp,
                UserEmail = t.UserEmail
            })
            .ToListAsync();
    }

    public async Task<List<UserTransactionSummaryDto>> GetMonthlyProcessedTransactionSummaryAsync()
    {
        var oneMonth = DateTime.UtcNow.AddMonths(-1);

        return await _dbContext.Transactions
            .Where(t => t.IsProcessed && t.Timestamp >= oneMonth)
            .GroupBy(t => t.UserEmail)
            .Select(g => new UserTransactionSummaryDto
            {
                UserEmail = g.Key,
                TotalAmount = g.Sum(t => t.Amount)
            })
            .ToListAsync();
        
    }
    
    #endregion

    #region Post

    public async Task MarkTransactionAsProcessedAsync(Guid transactionId)
    {
        var transaction = _dbContext.Transactions.FirstOrDefault(t => t.Id == transactionId);
        if (transaction == null)
        {
            throw new Exception("Транзакции такой нет");
        }

        transaction.IsProcessed = true;
        await _dbContext.SaveChangesAsync();
    }

    

    #endregion
    


}