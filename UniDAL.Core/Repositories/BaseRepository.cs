using UniDAL.Abstractions;

namespace UniDAL.Core.Repositories
{
    public abstract class BaseRepository<TEntity, TKey, TContext> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TContext : class, IDbContext
    {
        protected readonly TContext Context;

        protected BaseRepository(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public abstract TEntity GetById(TKey id);
        public abstract IQueryable<TEntity> GetAll();
        public abstract Task Insert(TEntity entity);
        public abstract Task Update(TEntity entity);
        public abstract Task Delete(TKey id);
    }
}
