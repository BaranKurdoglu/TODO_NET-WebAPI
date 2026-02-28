using dotnetDeneme.Data;
using dotnetDeneme.Dtos.Stock;
using dotnetDeneme.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnetDeneme.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;  //_context, veritabanıyla konuşmak için kullandığımız ApplicationDBContext nesnesi.

        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet] // GET /dotnetDeneme/stock → GetAll()
        public async Task<IActionResult> GetAll() //IActionResult; bir Controller methodunun hangi HTTP response döndüreceğini söyler, 200-400-404 vs.
        {
            var stocks = await _context.Stocks.ToListAsync();

            var stockDto = stocks.Select(s => s.ToStockDto());
            return Ok(stocks);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stocks = await _context.Stocks.FindAsync(id);
            if (stocks is null)
            {
                return NotFound();
            }

            return Ok(stocks.ToStockDto());
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDto();
            await _context.Stocks.AddAsync(stockModel);           // database'e giden herhangi bir şeye await eklemeliyiz.
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stockModel is null)
            {
                return NotFound();
            }

            stockModel.Symbol = updateDto.Symbol;
            stockModel.CompanyName = updateDto.CompanyName;
            stockModel.Purchase = updateDto.Purchase;
            stockModel.LastDiv = updateDto.LastDiv;
            stockModel.Industry = updateDto.Industry;
            stockModel.MarketCap = updateDto.MarketCap;

            await _context.SaveChangesAsync();

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stockModel is null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stockModel); // Remove, asenkron bir fonksiyon değil.

            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
// Database ile ilgili her şeyi asenkron yapmalıyız.
//EF = “C# nesneleri üzerinden DB işlemi yapma”. ORM
