using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManagementSystem.Data.Repos.IRepository;
//using BillingApp_Backend.Data.Repositories.IRepositories;

namespace TaskManagementSystem.Data.Repos.Repository
{
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    {
        protected DbContext dbContext;
        protected DbSet<T> dbSet;
        public GenericRepository(DbContext context, bool isMoc = false)
        {
            dbContext = context;
            dbSet = dbContext.Set<T>();
            if (!isMoc)
                dbContext.ChangeTracker.LazyLoadingEnabled = true;
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                await dbSet.AddAsync(entity);
                await dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<T>> AddRangeAsyc(IEnumerable<T> entity)
        {
            try
            {
                await dbSet.AddRangeAsync(entity);
                await dbContext.SaveChangesAsync();
                return entity.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                T entity = await dbSet.FindAsync(id);
                dbSet.Remove(entity);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Dispose()
        {
            try
            {
                dbContext?.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> ExistAsync(int id)
        {
            try
            {
                T entity = await GetByIdAsync(id);
                return entity != null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IQueryable<T> GetAll()
        {
            try
            {
                return dbSet;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<T>> GetAllByAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await dbSet.Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<T> GetFirstByAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await dbSet.Where(predicate).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                return await dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                //dbContext.Entry<T>(entity).State = EntityState.Modified;
                dbSet.Update(entity);
                await dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<T>> UpdateRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                dbSet.UpdateRange(entities);
                await dbContext.SaveChangesAsync();
                return entities.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<T> ReloadEntityAsync(T entity)
        {
            try
            {
                await dbContext.Entry(entity).ReloadAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DbContext GetContext()
        {
            return dbContext;
        }
    }
}
