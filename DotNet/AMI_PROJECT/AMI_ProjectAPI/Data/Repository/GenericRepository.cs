
using AMI_ProjectAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AMI_ProjectAPI.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly AmiContext _context;
        private DbSet<T> _dbSet;



        public GenericRepository(AmiContext amiContext)
        {
            _context = amiContext;
            _dbSet = amiContext.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
           await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
            await _context.SaveChangesAsync();
        }

        public object FirstOrDefault(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public Task GetByIdAsync(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(T entity)
        {

            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
