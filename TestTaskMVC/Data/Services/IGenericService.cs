using TestTaskMVC.Models;

namespace TestTaskMVC.Data.Services
{
    public interface IProductService
    {
        Task<Product> CreateAsync(Product source);

        Task<Product> UpdateAsync(long id, Product source);

        Task<Product> GetById(long id);

        Task<Product[]> GetAllAsync();

        Task DeleteAsync(long id);
    }
}
