using Audit.EntityFramework;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace AuditTestApi
{
    public class ProductDbContext : AuditDbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderItemId, oi.ProductId, oi.OrderId });

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);
        }
    }





    public class ProductEntity
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        public decimal Price { get; set; }

        public DateTime PublishDate { get; set; }
        public int Quantity { get; set; }

        [AuditIgnore]
        public decimal SpecialPrice { get; set; }
    }

    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }

    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
    }



}
