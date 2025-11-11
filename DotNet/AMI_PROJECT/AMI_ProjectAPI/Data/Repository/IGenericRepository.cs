
namespace AMI_ProjectAPI.Data.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        object FirstOrDefault(Func<object, bool> value);
        Task GetByIdAsync(Func<object, bool> value);
    }
}
