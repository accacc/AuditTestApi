namespace AuditTestApi
{
    public interface IProductService
    {
        IEnumerable<Order> GetProducts();
        Task<Order> GetAsync(int id);
        Task<int> InsertAsync();
        Task ReplaceAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<int> DeleteMultipleAsync(int[] ids);


    }
}
