using Audit.Core;

using Microsoft.EntityFrameworkCore;

namespace AuditTestApi
{
    public class ProductService : IProductService
    {
        private readonly ProductDbContext _dbContext;

        public ProductService(ProductDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }


        public async Task<IEnumerable<AuditEvent?>> GetAudits()
        {

            AuditService auditService = new AuditService();
            var list = await auditService.GetList();

            return list;
        }

        public IEnumerable<Order?> GetProducts()
        {

            AuditService auditService = new AuditService();
            var list = auditService.GetList();

            return _dbContext.Orders.ToList();
        }

        public async Task<Order?> GetAsync(int id)
        {
            var entity = await _dbContext.Orders.FindAsync(id);
            return entity;
        }

        public async Task<int> InsertAsync()
        {


            // Add sample products to the database
            _dbContext.Products.Add(new ProductEntity { Name = "Product A", Price = 10 });
            _dbContext.Products.Add(new ProductEntity { Name = "Product B", Price = 20 });
            _dbContext.SaveChanges();

            // Create a new order
            var order = new Order
            {
                OrderDate = DateTime.Now,
                OrderItems = new[]
                {
                new OrderItem { ProductId = 1, Quantity = 2 },
                new OrderItem { ProductId = 2, Quantity = 1 }
            }
            };

            // Add the order to the database
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            //await _dbContext.AddAsync(value);
            //await _dbContext.SaveChangesAsync();
            //return value.OrderId;
            return 1;
        }

        public async Task ReplaceAsync(int id)
        {
            var entity = await _dbContext.Orders.AsQueryable().Include(s => s.OrderItems).ThenInclude(i => i.Product).SingleOrDefaultAsync(s => s.OrderId == 1);

            if (entity != null)
            {

                entity.OrderDate = DateTime.Now;
                entity.OrderItems.First().Quantity = 3;
                entity.OrderItems.ElementAt(1).Quantity = 9;
                entity.OrderItems.First().Product.Name = "Product Yeni A";

                _dbContext.Update(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbContext.Products.FindAsync(id);
            if (entity != null)
            {
                _dbContext.Remove(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<int> DeleteMultipleAsync(int[] ids)
        {
            int c = 0;
            foreach (int id in ids)
            {
                c += await DeleteAsync(id) ? 1 : 0;
            }
            return c;
        }
    }
}
