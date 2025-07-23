using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
namespace Api.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionsController(
    ITransactionImportService importService,
    ITransactionQueryService queryService
    ) : ControllerBase
{
    [HttpPost("import")]
    public async Task<ActionResult> ImportTransaction()
    {
        await importService.ImportTransactionsAsync();
        return Ok("Принимайте транзакции друзья");
    }
    [HttpGet("unprocessed")]
    public async Task<IActionResult> GetUnprocessedTransactions()
    {
        var result = await queryService.GetUnprocessedTransactionsAsync();
        return Ok(result);
    }
}
