using MyCMS.DataLayer.Models;
using System.Linq.Expressions;

namespace MyCMS.Application.Repositories.Framework
{
    public interface IBaseGenericRepository<TEntity> where TEntity : BaseEntity
    {
        void Delete(TEntity entity);
        Task DeleteAsync(object id);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate = null);
        Task<TEntity?> GetByIdAsync(object id);
        Task InsertAsync(TEntity entity);
        Task SaveAsync();
        void Update(TEntity entity);
    }
}