using dotnetDeneme.Models;

namespace dotnetDeneme.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment commentModel);
        Task<Comment?> UpdateAsync(int id, Comment commentModel);
        Task<bool?> DeleteAsync(int id);

    }
}

// Önce Interface ekle - Sonra Repository'e implement et - En son Controller yazılır.