using System.Threading.Tasks;

namespace Application.Interfaces;

public interface ITransactionImportService
{
    Task ImportTransactionsAsync();

    Task GetUnProcessedTransactionsAsync();
}