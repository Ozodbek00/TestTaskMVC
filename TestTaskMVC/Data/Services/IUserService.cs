using TestTaskMVC.Models;

namespace TestTaskMVC.Data.Services
{
    public interface IUserService
    {
        Task<User> CreateAsync(User source);

        Task<User> UpdateAsync(long id, User source);

        Task<User> GetById(long id);

        Task<User[]> GetAllAsync();

        Task DeleteAsync(long id);
    }
}
