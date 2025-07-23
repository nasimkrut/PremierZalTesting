using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
namespace Api.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionsController(
    ITransactionService service
    ) : ControllerBase
{
    [HttpPost("import")]
    public async Task<ActionResult> ImportTransaction()
    {
        await service.ImportTransactionsAsync();
        return Ok("Принимайте транзакции друзья");
    }
    [HttpGet("unprocessed")]
    public async Task<IActionResult> GetUnprocessedTransactions()
    {
        var result = await service.GetUnprocessedTransactionsAsync();
        return Ok(result);
    }
    
    [HttpPost("{id}/process")]
    public async Task<NoContentResult> MarkTransactionAsProcessed(Guid id)
    {
        await service.MarkTransactionAsProcessedAsync(id);
        return NoContent();
    }

    [HttpGet("reports/monthly")]
    public async Task<IActionResult> GetMonthlyReportByUser()
    {
        var report = await service.GetMonthlyProcessedTransactionSummaryAsync();
        return Ok(report);
    }
}
