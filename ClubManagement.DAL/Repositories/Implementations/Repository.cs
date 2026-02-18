using ClubManagement.DAL.Data;
using ClubManagement.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ClubManagement.DAL.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _db;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.CountAsync(filter);
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task DeleteWhereAsync(Expression<Func<T, bool>> filter)
        {
            await _dbSet.Where(filter).ExecuteDeleteAsync();
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.Where(filter).ToListAsync();
        }

        public async Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector)
        {
            return await _dbSet.Where(filter).Select(selector).ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.FirstOrDefaultAsync(filter);
        }

        public async Task<TResult> GetAsync<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector)
        {
            return await _dbSet.Where(filter).Select(selector).FirstOrDefaultAsync();
        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
