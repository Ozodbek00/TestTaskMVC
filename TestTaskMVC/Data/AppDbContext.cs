using Microsoft.EntityFrameworkCore;
using TestTaskMVC.Models;

namespace TestTaskMVC.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<UserProduct> UsersProducts { get; set; }
    }
}
