using Application.Interfaces;
using Domain.Entities;
using Infrastructure;

namespace Application.Services;

public class CommandTransactionServices : ICommandTransactionService
{
    private readonly ApplicationDbContext _dbContext;

    public CommandTransactionServices(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
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


    public async Task<Transaction> GetTransactionByIdAsync(Guid transactionId)
    {
        var transaction = await Task.FromResult(_dbContext.Transactions.FirstOrDefault(t => t.Id == transactionId)!);
        if (transaction == null)
        {
            throw new Exception("Транзакции такой нет");
        }

        return transaction;
    }
}