using Application.Interfaces;
using Infrastructure;
using Infrastructure.BankApiClient;

namespace Application.Services;

public class TransactionCommandServices : ITransactionCommandService
{
    private readonly IBankApiClient _bankApiClient;
    private readonly ApplicationDbContext _dbContext;

    public TransactionCommandServices(IBankApiClient bankApiClient, ApplicationDbContext dbContext)
    {
        _bankApiClient = bankApiClient;
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

}