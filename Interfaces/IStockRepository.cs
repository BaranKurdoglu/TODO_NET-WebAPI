using dotnetDeneme.Dtos.Stock;
using dotnetDeneme.Helpers;
using dotnetDeneme.Models;

namespace dotnetDeneme.Interfaces
{
    public interface IStockRepository
    {
        Task<List<StockDto>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExist(int id);
    }
}
