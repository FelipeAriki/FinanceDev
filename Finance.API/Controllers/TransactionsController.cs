using FinanceDev.Application.DTOs;
using FinanceDev.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceDev.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IFinanceService _financeService;

        public TransactionsController(IFinanceService financeService)
        {
            _financeService = financeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? month, [FromQuery] int? year, [FromQuery] char? type)
        {
            var result = await _financeService.GetTransactionsAsync(month, year, type);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _financeService.GetTransactionByIdAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionCreateDTO dto)
        {

            try
            {
                await _financeService.CreateTransactionAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }


        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] TransactionCreateDTO dto)
        {

            try
            {
                await _financeService.UpdateTransactionAsync(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _financeService.GetTransactionByIdAsync(id);
            if (existing == null) return NotFound();

            await _financeService.DeleteTransactionAsync(id);
            return NoContent();
        }
    }
}
