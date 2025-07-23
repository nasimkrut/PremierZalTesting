using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces;

public interface ITransactionService
{
    Task ImportTransactionsAsync();
    Task<List<BankTransactionDto>> GetUnprocessedTransactionsAsync();
    Task MarkTransactionAsProcessedAsync(Guid transactionId);
}