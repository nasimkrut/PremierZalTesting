using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Api.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionsController(
    IImportTransactionsServices importServices,
    ITransactionCommandService commandService
    ) : ControllerBase
{
    [HttpPost("import")]
    public async Task<ActionResult> ImportTransaction()
    {
        await importServices.ImportTransactionsAsync();
        return Ok("Принимайте транзакции друзья");
    }
    
    [HttpPost("{id}/process")]
    public async Task<NoContentResult> MarkTransactionAsProcessed(Guid id)
    {
        await commandService.MarkTransactionAsProcessedAsync(id);
        return NoContent();
    }
}
