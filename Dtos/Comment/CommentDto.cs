using System.ComponentModel.DataAnnotations;

namespace dotnetDeneme.Dtos.Comment
{
    public class CommentDto
    {
        public int? StockID { get; set; } //key

        [Key]
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
