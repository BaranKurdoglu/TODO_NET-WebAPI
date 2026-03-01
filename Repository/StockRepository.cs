using dotnetDeneme.Data;
using dotnetDeneme.Dtos.Stock;
using dotnetDeneme.Interfaces;
using dotnetDeneme.Mappers;
using dotnetDeneme.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetDeneme.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;

        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<StockDto>> GetAllAsync()
        {
            return await _context.Stocks.AsNoTracking().Select(s => s.ToStockDto()).ToListAsync();
        }
    }
}
