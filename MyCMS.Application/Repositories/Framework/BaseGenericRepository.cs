using Microsoft.EntityFrameworkCore;
using MyCMS.DataLayer.Contexts;
using MyCMS.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyCMS.Application.Repositories.Framework
{
    public abstract class BaseGenericRepository<TEntity> : IBaseGenericRepository<TEntity> where TEntity : BaseEntity
    {

        private readonly MyCmsDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseGenericRepository(MyCmsDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        // --- Get ---
        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);

            return await query.ToListAsync();
        }

        // --- GetById ---
        public virtual async Task<TEntity?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        // --- Insert ---
        public virtual async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        // --- Update ---
        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        // --- Delete (by entity) ---
        public virtual void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }

        // --- Delete (by id) ---
        public virtual async Task DeleteAsync(object id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
                Delete(entity);
        }

        // --- Save changes ---
        public virtual async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
