namespace TestTaskMVC.Data.Services
{
    public interface IGenericService<TSource> where TSource: class
    {
        Task<TSource> CreateAsync(TSource source);

        Task<TSource> UpdateAsync(long id, TSource source);

        Task<TSource> GetById(long id);

        Task<TSource[]> GetAllAsync();

        Task DeleteAsync(long id);
    }
}
