using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/transactions")]
public class ReportController(
    IQueryTransactionServices queryServices
) : ControllerBase
{
    [HttpGet("unprocessed")]
    public async Task<IActionResult> GetUnprocessedTransactions()
    {
        var result = await queryServices.GetUnprocessedTransactionsAsync();
        return Ok(result);
    }

    [HttpGet("reports/monthly")]
    public async Task<IActionResult> GetMonthlyReportByUser()
    {
        var report = await queryServices.GetMonthlyProcessedTransactionSummaryAsync();
        return Ok(report);
    }
}