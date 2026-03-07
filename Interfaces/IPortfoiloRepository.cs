using dotnetDeneme.Models;

namespace dotnetDeneme.Interfaces
{
    public interface IPortfoiloRepository
    {
        Task<List<Stock>> GetUserPortfoilo(AppUser user);
    }
}
