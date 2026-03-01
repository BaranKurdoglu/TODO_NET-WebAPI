using dotnetDeneme.Data;
using dotnetDeneme.Dtos.Stock;
using dotnetDeneme.Interfaces;
using dotnetDeneme.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnetDeneme.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepo;

        public StockController(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }


        [HttpGet] // GET /dotnetDeneme/stock → GetAll()
        public async Task<IActionResult> GetAll() //IActionResult; bir Controller methodunun hangi HTTP response döndüreceğini söyler, 200-400-404 vs.
        {
            var stocks = await _stockRepo.GetAllAsync();
            var stockDto = stocks;
            return Ok(stocks);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stocks = await _stockRepo.GetByIdAsync(id);
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
            await _stockRepo.CreateAsync(stockModel);           // database'e giden herhangi bir şeye await eklemeliyiz.


            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel = await _stockRepo.UpdateAsync(id, updateDto);

            if (stockModel is null)
            {
                return NotFound();
            }

            return Ok(stockModel.ToStockDto());
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel = await _stockRepo.DeleteAsync(id);

            if (stockModel is null)
            {
                return NotFound();
            }



            return NoContent();
        }

    }
}
// Database ile ilgili her şeyi asenkron yapmalıyız.
//EF = “C# nesneleri üzerinden DB işlemi yapma”. ORM
