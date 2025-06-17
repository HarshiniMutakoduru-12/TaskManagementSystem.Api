using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Data.Repos.IRepository
{
    public interface IGenericRepository<T>
    {
        Task<T> AddAsync(T entity);
        Task<List<T>> AddRangeAsyc(IEnumerable<T> entity);
        Task DeleteAsync(int id);
        void Dispose();
        Task<bool> ExistAsync(int id);
        IQueryable<T> GetAll();      
        Task<List<T>> GetAllByAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetFirstByAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(int id);
        Task<T> UpdateAsync(T entity);
        Task<List<T>> UpdateRangeAsync(IEnumerable<T> entities);
        Task<T> ReloadEntityAsync(T entity);
        DbContext GetContext();
    }
}
