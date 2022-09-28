using System.Linq.Expressions;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task AddEntity(TEntity entity);
        Task<TEntity> GetEntityById(Guid entityId);
        Task<List<TEntity>> GetAllEntities();
        Task RemoveEntity(Guid entityId);
        Task UpdateEntity(TEntity entity);
        Task<IEnumerable<TEntity>> SearchEntity(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}
