using TestTaskMVC.Models;

namespace TestTaskMVC.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();

            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                context.Users.AddRange(new List<User>()
                {
                    new User()
                    {
                        Username = "Alex",
                        Password = "Alexgreat",
                        Role = Enums.UserRole.Admin,
                    },
                    new User()
                    {
                        Username = "Kim",
                        Password ="kimkim",
                        Role = Enums.UserRole.User
                    }
                });
            }

            if (!context.Products.Any())
            {
                context.Products.AddRange(new List<Product>()
                {
                    new Product()
                    {
                        Title = "HDD 1T",
                        Quantity = 55,
                        Price = 74.09
                    },
                    new Product()
                    {
                        Title = "HDD SSD 512GB",
                        Quantity = 102,
                        Price = 190.99
                    },
                    new Product()
                    {
                        Title = "RAM DDR4 16GB",
                        Quantity = 47,
                        Price = 80.32
                    },
                });
            }

            context.SaveChanges();
        }
    }
}
