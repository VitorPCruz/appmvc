using System.Linq.Expressions;
using System.Reflection.Metadata;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly DataDbContext? Context;
        protected readonly DbSet<TEntity>? DbSet;

        public Repository(DataDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> SearchEntity(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> GetEntityById(Guid entityId)
        {
            return await DbSet.FindAsync(entityId);
        }
       
        public virtual async Task<List<TEntity>> GetAllEntities()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task AddEntity(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task UpdateEntity(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task RemoveEntity(Guid entityId)
        {
            DbSet.Remove(new TEntity { Id = entityId });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
