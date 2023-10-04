using Microsoft.EntityFrameworkCore;

namespace FlowerSaleAPI.Models
{
    public class ShopContext:DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //difine entity with one PK-FK connection
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(a => a.Category)
                .HasForeignKey(a => a.CategoryId);

            modelBuilder.Seed();
        }

           public DbSet<Product> Products { get; set; }

           public DbSet<Category> Category { get; set; }
        
    }
}
