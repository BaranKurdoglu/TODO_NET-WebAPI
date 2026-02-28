using dotnetDeneme.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetDeneme.Data
{
    public class ApplicationDBContext : DbContext // dotnetin classı. implement ediyoruz.
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) //Bu sınıf oluşturulurken bana Options ver.
            : base(dbContextOptions) // base = javadaki super. Options'ı DbContext'e gönderiyorum.
        {
        }

        public DbSet<Stock> Stocks { get; set; } //DbSet - database tablosu oluştur.

        public DbSet<Comment> Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
