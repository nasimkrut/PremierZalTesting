using Application.DTOs;
using Application.Interfaces;
using Infrastructure;
using Infrastructure.BankApiClient;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class QueryTransactionServices : IQueryTransactionServices
{
    private readonly IBankApiClient _bankApiClient;
    private readonly ApplicationDbContext _dbContext;

    public QueryTransactionServices(IBankApiClient bankApiClient, ApplicationDbContext dbContext)
    {
        _bankApiClient = bankApiClient;
        _dbContext = dbContext;
    }
    
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
}