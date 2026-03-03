using dotnetDeneme.Dtos.Comment;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetDeneme.Dtos.Stock
{
    public class StockDto    // DTO -> Data Transfer Object. Katmanlar - API arasında taşınan request/response modelidir.
    {                        
        [Key]
        public int Id { get; set; } 

        public string Symbol { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        public decimal Purchase { get; set; }

        public decimal LastDiv { get; set; }

        public string Industry { get; set; } = string.Empty;

        public long MarketCap { get; set; }

        public List<CommentDto> Comments { get; set; }
    }
}
