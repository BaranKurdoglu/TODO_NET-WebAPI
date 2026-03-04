using dotnetDeneme.Data;
using dotnetDeneme.Interfaces;
using dotnetDeneme.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetDeneme.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();

            return commentModel;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            var existingComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (existingComment is null)
            {
                return null;
            }

            _context.Comments.Remove(existingComment);
            return await _context.SaveChangesAsync() is not 0; //Databse'de etkilenen satır sayısı 0 değilse true döndür.

        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context
                .Comments
                .ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            var existingComment = await _context.Comments.FindAsync(id);

            if (existingComment is null)
            {
                return null;
            }

            existingComment.Title = commentModel.Title;
            existingComment.Content = commentModel.Content;

            await _context.SaveChangesAsync();
            return existingComment;
        }

        
    }
}
