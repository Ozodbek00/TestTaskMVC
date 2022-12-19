using Microsoft.EntityFrameworkCore;
using TestTaskMVC.Models;

namespace TestTaskMVC.Data.Services
{
    public class ProductService : IGenericService<Product>
    {
        private readonly AppDbContext dbContext;
        private readonly IConfiguration configuration;
        private readonly HttpContext httpContext;

        public ProductService(AppDbContext dbContext, IConfiguration configuration, HttpContext httpContext)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
            this.httpContext = httpContext;
        }

        public async Task<Product> CreateAsync(Product source)
        {
            var product = await dbContext.Products
                        .FirstOrDefaultAsync(u => u.Title.Equals(source.Title) &&
                        u.Price.Equals(source.Title));

            if (product is not null)
                throw new Exception();

            source.TotalPrice = (decimal)(source.Quantity * source.Price) * (1 + int.Parse(configuration.GetConnectionString("VAN")));

            var result =  (await dbContext.Products.AddAsync(source)).Entity;
            await dbContext.SaveChangesAsync();

            return result;
        }

        public async Task DeleteAsync(long id)
        {
            var product = await dbContext.Products.FirstOrDefaultAsync(u => u.Id == id);

            if (product is not null)
            {
                await dbContext.UsersProducts.AddAsync(new UserProduct()
                {
                    UserId = long.Parse(httpContext?.User.Claims.FirstOrDefault(p => p.Type == "Id")?.Value),
                    ProductId = product.Id,
                    UpdatedAt = DateTime.UtcNow
            });
                dbContext.Products.Remove(product);
            }

            await dbContext.SaveChangesAsync();
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
            {
                productCheck.Quantity += source.Quantity;
                productCheck.TotalPrice += (decimal) source.Price * source.Quantity;
            }

            product.Title = source.Title;
            product.Price = source.Price;
            product.Quantity = source.Quantity;
            product.TotalPrice = (decimal)(source.Quantity * source.Price);

            dbContext.Products.Update(product);
            await dbContext.SaveChangesAsync();

            return product;
        }
    }
}
