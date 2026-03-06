using dotnetDeneme.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotnetDeneme.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser> // dotnetin classı. implement ediyoruz.
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

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },

                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
