using dotnetDeneme.Models;

namespace dotnetDeneme.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolioAsync(AppUser user);
        Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio);
    }
}
