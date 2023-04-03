namespace AuditTestApi
{
    public interface IProductService
    {
        IEnumerable<ProductEntity> GetProducts();
        Task<ProductEntity> GetAsync(int id);
        Task<int> InsertAsync(ProductEntity value);
        Task ReplaceAsync(int id, ProductEntity value);
        Task<bool> DeleteAsync(int id);
        Task<int> DeleteMultipleAsync(int[] ids);


    }
}
