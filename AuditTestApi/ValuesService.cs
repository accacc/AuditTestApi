using Audit.Core;

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

        public IEnumerable<ProductEntity?> GetProducts()
        {

            AuditService auditService = new AuditService();
            var list = auditService.GetList();

            return _dbContext.Products.ToList();
        }

        public async Task<ProductEntity?> GetAsync(int id)
        {
            var entity = await _dbContext.Products.FindAsync(id);
            return entity;
        }

        public async Task<int> InsertAsync(ProductEntity value)
        {
            await _dbContext.AddAsync(value);
            await _dbContext.SaveChangesAsync();
            return value.Id;
        }

        public async Task ReplaceAsync(int id, ProductEntity value)
        {
            var entity = await _dbContext.Products.FindAsync(id);

            if (entity != null)
            {
                entity.SpecialPrice = value.SpecialPrice;
                entity.Price = value.Price;
                entity.Quantity = value.Quantity;
                entity.PublishDate = value.PublishDate;
                entity.Name = value.Name;

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
