using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetDeneme.Models
{
    public class Stock
    {
        [Key] // [] -> Her zaman bir altındakini işaret eder, ilk prop'a kadar olanları kapsar.
        public int Id { get; set; } 

        public string Symbol { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")] //dotnet EF
        public decimal Purchase { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal LastDiv { get; set; }

        public string Industry { get; set; } = string.Empty;

        public long MarketCap { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>(); // 1 Stock'un birden fazla Comment'ı olabilir. 1 to Many ilişkisi.

        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>(); // Many to Many (User-Stock)
    }
}
