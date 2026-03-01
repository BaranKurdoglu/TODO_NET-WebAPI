using dotnetDeneme.Dtos.Stock;
using dotnetDeneme.Models;

namespace dotnetDeneme.Interfaces
{
    public interface IStockRepository
    {
        Task<List<StockDto>> GetAllAsync();
    }
}
