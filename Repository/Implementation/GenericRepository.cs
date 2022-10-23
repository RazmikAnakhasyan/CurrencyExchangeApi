using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Repository.Abstraction;
using System.Linq.Expressions;

namespace Repository.Implementation
{

    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class 
    {
        private readonly IServiceScopeFactory scopeFactory;
        public GenericRepository(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public virtual Task<IEnumerable<T>> GetAllAsync(Func<T, bool> predicate)
        {
            var scope = scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<CurrencyExchangeContext>();
            
            return Task.FromResult(dbContext.Set<T>().Where(predicate));
        }

        public virtual async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null, bool? disableTracking = null)
        {
            var scope = scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<CurrencyExchangeContext>();
            var query = dbContext.Set<T>().AsQueryable();

            if (disableTracking == true)
                query = query.AsNoTracking();

            if (includes != null)
                query = includes(query).IgnoreAutoIncludes();

            return Task.FromResult(query.Where(filter)).Result.ToList();
        }
        public virtual IEnumerable<T> GetAll()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<CurrencyExchangeContext>();
                return dbContext.Set<T>();
            }
        }
        public virtual T Get(object Id)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<CurrencyExchangeContext>();
                return dbContext.Set<T>().Find(Id);
            }
        }
        public virtual async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<CurrencyExchangeContext>();
                return await dbContext.Set<T>().FirstOrDefaultAsync(predicate);
            }
        }
        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null, bool? disableTracking = null)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<CurrencyExchangeContext>();
                var query = dbContext.Set<T>().AsQueryable();

                if (disableTracking == true)
                    query = query.AsNoTracking();

                if (includes != null)
                    query = includes(query).IgnoreAutoIncludes();
                var result = await query.Where(filter).FirstOrDefaultAsync();
                return result;
            }
        }
        public virtual async Task AddAsync(T entity)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<CurrencyExchangeContext>();
                await dbContext.Set<T>().AddAsync(entity);
                await dbContext.SaveChangesAsync();
            }
        }
        public virtual async Task UpdateAsync(T entity)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<CurrencyExchangeContext>();
                dbContext.Set<T>().Update(entity);
                await dbContext.SaveChangesAsync();
            }
        }
        public virtual async Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<CurrencyExchangeContext>();
                var entity = await GetAsync(predicate);

                dbContext.Set<T>().Remove(entity);
                dbContext.SaveChanges();
            }
        }
        public virtual void Delete(T entity)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<CurrencyExchangeContext>();
                dbContext.Set<T>().Remove(entity);
                dbContext.SaveChanges();
            }
        }
    }
}
