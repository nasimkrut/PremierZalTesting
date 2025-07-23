using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
namespace Api.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionsController(ITransactionImportService importService) : ControllerBase
{
    [HttpPost("importTransaction")]
    public async Task<ActionResult> ImportTransaction()
    {
        await importService.ImportTransactionsAsync();
        return Ok("Принимайте транзакции друзья");
    }
}
