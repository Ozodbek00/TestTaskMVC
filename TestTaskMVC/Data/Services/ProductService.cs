using Microsoft.EntityFrameworkCore;
using TestTaskMVC.Models;

namespace TestTaskMVC.Data.Services
{
    public class ProductService : IGenericService<Product>
    {
        private readonly AppDbContext dbContext;

        public ProductService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Product> CreateAsync(Product source)
        {
            var product = await dbContext.Products
                        .FirstOrDefaultAsync(u => u.Title.Equals(source.Title) &&
                        u.Price.Equals(source.Title));

            if (product is not null)
                throw new Exception();

            return (await dbContext.Products.AddAsync(source)).Entity;
        }

        public async Task DeleteAsync(long id)
        {
            var product = await dbContext.Products.FirstOrDefaultAsync(u => u.Id == id);

            if (product is not null)
                dbContext.Products.Remove(product);
        }

        public async Task<Product[]> GetAllAsync()
        {
            return await dbContext.Products.ToArrayAsync();
        }

        public async Task<Product> GetById(long id)
        {
            return await dbContext.Products.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Product> UpdateAsync(long id, Product source)
        {
            var product = await dbContext.Products.FirstOrDefaultAsync(u => u.Id == id);

            if (product is null)
                throw new Exception();

            var productCheck = await dbContext.Products
                        .FirstOrDefaultAsync(u => u.Title.Equals(source.Title) &&
                        u.Price.Equals(source.Price) && u.Id != source.Id);

            if (productCheck is not null)
                productCheck.Quantity += source.Quantity;

            product.Title = source.Title;
            product.Price = source.Price;
            product.Quantity = source.Quantity;

            dbContext.Products.Update(product);

            return product;
        }
    }
}
