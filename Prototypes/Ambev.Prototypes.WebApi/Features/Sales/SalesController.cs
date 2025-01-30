using Ambev.Prototypes.Domain.Entities;
using Ambev.Prototypes.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.Prototypes.WebApi.Features.Sales
{
    /// <summary>
    /// Controller for Sales operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : Controller
    {
        private readonly SaleService _saleService;

        /// <summary>
        /// Initializes a new instance of SalesController
        /// </summary>
        /// <param name="saleService">The saleService instance</param>
        public SalesController(SaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sales = await _saleService.GetAllSalesAsync();
            return Ok(sales);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var sale = await _saleService.GetSaleByIdAsync(id);
            if (sale == null)
                return NotFound();
            return Ok(sale);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Sale sale)
        {
            await _saleService.AddSaleAsync(sale);
            return CreatedAtAction(nameof(GetById), new { id = sale.Id }, sale);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Sale sale)
        {
            if (id != sale.Id)
                return BadRequest("ID mismatch");

            await _saleService.UpdateSaleAsync(sale);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _saleService.DeleteSaleAsync(id);
            return NoContent();
        }
    }
}
