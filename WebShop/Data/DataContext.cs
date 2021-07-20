using Microsoft.EntityFrameworkCore;
using WebShop.Entities;

namespace WebShop.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Shop> Shops { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shop>()
            .HasMany(s => s.Products)
            .WithOne(pr => pr.Shop)
            .HasForeignKey(si => si.ShopId);
        }
    }
}
