using Application.DTOs;

namespace Application.Interfaces;

public interface IQueryTransactionServices
{
    Task<List<BankTransactionDto>> GetUnprocessedTransactionsAsync();
    Task<List<UserTransactionSummaryDto>> GetMonthlyProcessedTransactionSummaryAsync();
}