using Microsoft.EntityFrameworkCore;
using NerdyGadgets.Models;
using NerdyGadgets.Dtos;

namespace NerdyGadgets.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CouponCode> CouponCodes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSpec> ProductSpecs { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
                
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique keys.
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Code)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Composite Keys.
            modelBuilder.Entity<OrderLine>()
                .HasKey(o => new
                {
                    o.OrderId,
                    o.ProductNumber
                });

            modelBuilder.Entity<ProductSpec>()
                .HasKey(p => new
                {
                    p.ProductNumber,
                    p.SpecName
                });

        }
                
        public DbSet<NerdyGadgets.Dtos.UserAddressDto> UserAddressDto { get; set; }
    }
}
