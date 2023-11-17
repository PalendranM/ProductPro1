using Microsoft.EntityFrameworkCore;
using ProductPro.Models;
using ProductPro.Models.Dto;

namespace ProductPro.Data
{
    public class ProductDbContext: DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options):base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<LocalUser> LocalUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Name",
                    Detail = "test",
                    Qty = 5
                },
                new Product
                {
                    Id = 2,
                    Name = "Name2",
                    Detail = "test2",
                    Qty = 10
                },
                new Product
                {
                    Id = 3,
                    Name = "Name3",
                    Detail = "test3",
                    Qty = 10
                }
                );
        }
    }
}
