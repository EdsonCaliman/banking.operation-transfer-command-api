using Banking.Operation.Transfer.Command.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Banking.Operation.Transfer.Command.Infra.Data.Repositories
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        protected BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public IQueryable<T> Get()
        {
            return _dbSet.AsNoTracking();
        }

        public async Task<T> FindOne(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.SingleOrDefaultAsync(predicate);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _dbSet.Update(entity);
            _context.SaveChanges();
        }
    }
}
