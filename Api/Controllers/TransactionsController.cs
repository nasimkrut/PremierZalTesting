using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Api.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionsController(
    IImportTransactionsServices importServices,
    ICommandTransactionService service
    ) : ControllerBase
{
    [HttpPost("import")]
    public async Task<ActionResult> ImportTransaction()
    {
        await importServices.ImportTransactionsAsync();
        return Ok("Принимайте транзакции друзья");
    }
    
    [HttpPost("{id}/process")]
    public async Task<ActionResult> MarkTransactionAsProcessed(Guid id)
    {
        var transaction = await service.GetTransactionByIdAsync(id);
        if (transaction.IsProcessed)
            return Conflict($"Транзакция {id} уже была обработана ранее");
            
        await service.MarkTransactionAsProcessedAsync(id);
        return Ok("Помечено");
    }
}
