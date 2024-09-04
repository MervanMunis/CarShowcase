using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebAPI.Data;

namespace WebAPI.Repositories.Base
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly RepositoryDbContext _context;

        public RepositoryBase(RepositoryDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll(bool trackChanges) =>
            trackChanges ? 
            _context.Set<T>() :
            _context.Set<T>().AsNoTracking();        

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            trackChanges ? 
            _context.Set<T>().Where(expression) :
            _context.Set<T>().Where(expression).AsNoTracking();

        public async Task CreateAsync(T entity) => 
            await _context.Set<T>().AddAsync(entity);


        public async Task CreateAsync(IEnumerable<T> entities) =>
            await _context.Set<T>().AddRangeAsync(entities);

        public void Update(T entity) =>
            _context.Set<T>().Update(entity);

        public void UpdateRange(IEnumerable<T> entities) =>
            _context.Set<T>().UpdateRange(entities);

        public void Delete(T entity) =>
            _context.Set<T>().Remove(entity);

        public void Delete(IEnumerable<T> entities) =>
            _context.Set<T>().RemoveRange(entities);
    }
}
