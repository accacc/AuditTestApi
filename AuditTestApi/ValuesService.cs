namespace AuditTestApi
{
    public class ProductService : IProductService
    {
        private readonly ProductDbContext _dbContext;

        public ProductService(ProductDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IEnumerable<ProductEntity?> GetProducts()
        {
            return _dbContext.Products;
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
                entity = value;
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
