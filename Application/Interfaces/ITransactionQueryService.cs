using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface ITransactionQueryService
{
    Task<List<BankTransactionDto>> GetUnprocessedTransactionsAsync();
}