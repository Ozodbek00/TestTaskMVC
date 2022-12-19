using Microsoft.EntityFrameworkCore;
using TestTaskMVC.Models;

namespace TestTaskMVC.Data.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext dbContext;

        public UserService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> CreateAsync(User source)
        {
            var userLogin = await dbContext.Users
                        .FirstOrDefaultAsync(u => u.Username.Equals(source.Username) &&
                        u.Password.Equals(source.Password));

            if (userLogin is not null)
                throw new Exception();

            User result = (await dbContext.Users.AddAsync(source)).Entity;

            await dbContext.SaveChangesAsync();

            return result;
        }

        public async Task DeleteAsync(long id)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user is not null)
                dbContext.Users.Remove(user);

            await dbContext.SaveChangesAsync();
        }

        public async Task<User[]> GetAllAsync()
        {
            return await dbContext.Users.ToArrayAsync();
        }

        public async Task<User> GetById(long id)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> UpdateAsync(long id, User source)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
                throw new Exception();

            var userLogin = await dbContext.Users
                        .FirstOrDefaultAsync(u => u.Username.Equals(source.Username) &&
                        u.Password.Equals(source.Password) && u.Id != source.Id);

            if (userLogin is not null)
                throw new Exception();

            user.Username = source.Username;
            user.Password = source.Password;
            user.Role = source.Role;

            dbContext.Users.Update(user);

            await dbContext.SaveChangesAsync();

            return user;
        }
    }
}
