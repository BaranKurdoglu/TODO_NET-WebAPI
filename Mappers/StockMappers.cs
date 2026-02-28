using dotnetDeneme.Dtos.Stock;
using dotnetDeneme.Models;

namespace dotnetDeneme.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stokModel)
        {
            return new StockDto
            {
                Id = stokModel.Id,
                Symbol = stokModel.Symbol,
                CompanyName = stokModel.CompanyName,
                Purchase = stokModel.Purchase,
                Industry = stokModel.Industry,
                LastDiv = stokModel.LastDiv,
                MarketCap = stokModel.MarketCap
            };
        }
    }
}
