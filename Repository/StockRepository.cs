using dotnetDeneme.Data;
using dotnetDeneme.Dtos.Stock;
using dotnetDeneme.Helpers;
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

        public async Task<List<StockDto>> GetAllAsync(QueryObject query)
        {
            var stocks = _context.Stocks
                .AsNoTracking()                    //Sorguyu database düzeyinde çalıştırarak bellekteki gereksiz veriyi önledik.
                .Include(c => c.Comments)       //Bu bilgiler üzerinde düzenleme-silme işlemleri yapıldığında, izlemeyi kapatmak için kullanırız.(AsNpTracking)
                .AsQueryable();             //EF Core'da databaseden çekilen her şey izlenir.


            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }


            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }


            return await stocks.Select(s => s.ToStockDto()).ToListAsync(); // ToStockDto SQL’e çevrilemediği için önce filtreleyip sonra DTO’ya mapliyoruz.
        }

        // EF Core’da önce entity query üzerinde Include ile ilişkili verileri ekletirsin, sonra Select ile DTO’ya dönüştürürsün. 

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks
                .AsNoTracking()
                .Include(c => c.Comments)
                .FirstOrDefaultAsync(x => x.Id == id); //Id'li işlemlerde FirstOrDefeult() kullan.
        }

        public Task<bool> StockExist(int id)
        {
            return _context.Stocks.AnyAsync(s => s.Id == id);
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
