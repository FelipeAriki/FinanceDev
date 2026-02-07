using FinanceDev.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceDev.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DashboardController : ControllerBase
{
    private readonly IFinanceService _financeService;

    public DashboardController(IFinanceService financeService)
    {
        _financeService = financeService;
    }

    [HttpGet("summary")]
    public async Task<IActionResult> Summary([FromQuery] int month, [FromQuery] int year)
    {
        var result = await _financeService.GetDashboardAsync(month, year);
        return Ok(result);
    }


    [HttpGet("by-category")]
    public async Task<IActionResult> ByCategory([FromQuery] int? month, [FromQuery] int? year)
    {
        var result = await _financeService.GetTotalsByCategoryAsync(month, year);
        return Ok(result);
    }
}
