using Domain.Entities;

namespace Application.Interfaces;

public interface ICommandTransactionService
{
    Task MarkTransactionAsProcessedAsync(Guid transactionId);
    Task<Transaction> GetTransactionByIdAsync(Guid transactionId);
}