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


}
