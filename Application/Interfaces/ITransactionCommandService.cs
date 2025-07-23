namespace Application.Interfaces;

public interface ITransactionCommandService
{
    Task MarkTransactionAsProcessedAsync(Guid transactionId);

}