using Project20180804.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Project20180804.Core.DAL.Repositorys
{
    public interface IRepository<T> where T : EntityBase
    {
        T GetById(object id);

        Task<T> GetByIdAsync(object id);

        List<T> GetAll();

        Task<List<T>> GetAllAsync();

        List<T> ToList(Expression<Func<T, bool>> predicate);

        Task<List<T>> ToListAsync(Expression<Func<T, bool>> predicate);

        T Insert(T entity);

        Task<T> InsertAsync(T entity);

        void Insert(IEnumerable<T> entities);

        Task<List<T>> InsertAsync(IEnumerable<T> entities);

        T Update(T entity);

        Task<T> UpdateAsync(T entity);

        void Update(IEnumerable<T> entities);

        Task UpdateAsync(IEnumerable<T> entities);

        void Delete(T entity);

        Task DeleteAsync(T entity);

        void Delete(IEnumerable<T> entities);

        Task DeleteAsync(IEnumerable<T> entities);

        int Count(Expression<Func<T, bool>> predicate);

        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        T IncludeSingleOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths);

        Task<T> IncludeSingleOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths);

        Task<List<T>> IncludeToListAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths);

        T SingleOrDefault(Expression<Func<T, bool>> predicate);

        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);

        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        void Include(params Expression<Func<T, object>>[] paths);

        IQueryable<T> Table { get; }

        IQueryable<T> TableNoTracking { get; }
    }
}
