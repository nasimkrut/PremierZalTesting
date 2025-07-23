using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class TransactionQueryService : ITransactionQueryService
{
    private readonly ApplicationDbContext _dbContext;

    public TransactionQueryService(ApplicationDbContext dbContext)
    {
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
}

