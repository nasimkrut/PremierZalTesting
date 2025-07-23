using Domain.Entities;

namespace Infrastructure.BankApiClient;

public interface IBankApiClient
{
    public Task<List<BankTransaction>> GetRecentTransactionsAsync();
}