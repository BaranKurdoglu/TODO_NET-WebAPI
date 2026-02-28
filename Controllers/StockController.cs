using dotnetDeneme.Data;
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
    }
}

//EF = “C# nesneleri üzerinden DB işlemi yapma”. ORM
