namespace Application.Interfaces;

public interface ICommandTransactionService
{
    Task MarkTransactionAsProcessedAsync(Guid transactionId);
}