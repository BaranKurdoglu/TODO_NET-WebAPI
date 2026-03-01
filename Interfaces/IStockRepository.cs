using dotnetDeneme.Dtos.Stock;
using dotnetDeneme.Models;

namespace dotnetDeneme.Interfaces
{
    public interface IStockRepository
    {
        Task<List<StockDto>> GetAllAsync();
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
        Task<Stock?> DeleteAsync(int id);
    }
}
