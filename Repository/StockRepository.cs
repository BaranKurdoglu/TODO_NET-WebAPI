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

        public StockRepository(ApplicationDBContext context) //_context, veritabanıyla konuşmak için kullandığımız ApplicationDBContext nesnesi.
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stockModel is null)
            {
                return null; //nulable (?) yaptığımız için NotFound yerine null dedik.
            }

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<StockDto>> GetAllAsync()
        {
            return await _context.Stocks
                .AsNoTracking()                    //Sorguyu database düzeyinde çalıştırarak bellekteki gereksiz veriyi önledik.
                .Select(s => s.ToStockDto())     //EF Core'da databaseden çekilen her şey izlenir.
                .ToListAsync();                //Bu bilgiler üzerinde düzenleme-silme işlemleri yapıldığında, izlemeyi kapatmak için kullanırız.(AsNpTracking)
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks
                .AsNoTracking() 
                .FirstOrDefaultAsync(x => x.Id == id); //Id'li işlemlerde FirstOrDefeult() kullan.
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var existingStock = await _context.Stocks
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existingStock is null)
            {
                return null;
            }

            existingStock.Symbol = stockDto.Symbol;
            existingStock.CompanyName = stockDto.CompanyName;
            existingStock.Purchase = stockDto.Purchase;
            existingStock.LastDiv = stockDto.LastDiv;
            existingStock.Industry = stockDto.Industry;
            existingStock.MarketCap = stockDto.MarketCap;

            await _context.SaveChangesAsync();

            return existingStock;
        }
    }
}
