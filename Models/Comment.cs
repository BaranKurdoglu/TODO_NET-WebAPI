using System.ComponentModel.DataAnnotations;

namespace dotnetDeneme.Models
{
    public class Comment
    {
        public int? StockID { get; set; } //key
        public Stock? Stock { get; set; } // Hangi Comment'ın hangi Stock'ta olduğunu görebilmek için. navigation

        [Key] // primary key
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;

    }
}
