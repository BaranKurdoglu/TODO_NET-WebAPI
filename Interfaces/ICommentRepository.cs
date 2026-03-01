using dotnetDeneme.Models;

namespace dotnetDeneme.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
    }
}
