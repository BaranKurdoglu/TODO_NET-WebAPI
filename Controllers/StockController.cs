using dotnetDeneme.Data;
using dotnetDeneme.Dtos.Stock;
using dotnetDeneme.Mappers;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAll() //IActionResult; bir Controller methodunun hangi HTTP response döndüreceğini söyler, 200-400-404 vs.
        {
            var stocks = _context.Stocks.ToList()
             .Select(s => s.ToStockDto());
            return Ok(stocks);
        }


        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stocks = _context.Stocks.Find(id);
            if (stocks is null)
            {
                return NotFound();
            }

            return Ok(stocks.ToStockDto());
        }

        [HttpPost]
        
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDto();
            _context.Stocks.Add(stockModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }
    }
}

//EF = “C# nesneleri üzerinden DB işlemi yapma”. ORM
