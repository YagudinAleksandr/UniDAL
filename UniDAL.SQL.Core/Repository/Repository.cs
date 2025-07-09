using Microsoft.EntityFrameworkCore;
using UniDAL.SQL.Abstractions;

namespace UniDAL.SQL.Core
{
    /// <summary>
    /// Реализация базового репозитория CRUD-операций
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    /// <typeparam name="TKey">Тип идентификатора сущности</typeparam>
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        /// <inheritdoc cref="DbContext"/>
        protected readonly DbContext _context;

        /// <inheritdoc cref="DbSet{TEntity}"/>
        protected readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Конструктор репозитория
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public Repository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Получить сущность по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        public virtual async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Получить все сущности
        /// </summary>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        /// <summary>
        /// Добавить новую сущность
        /// </summary>
        /// <param name="entity">Добавляемая сущность</param>
        public virtual async Task AddAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await _dbSet.AddAsync(entity);
        }

        /// <summary>
        /// Обновить существующую сущность
        /// </summary>
        /// <param name="entity">Сущность для обновления</param>
        public virtual async Task UpdateAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.Entry(entity).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        /// <summary>
        /// Удалить сущность по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        public virtual async Task DeleteAsync(TKey id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException($"Сущность с ID {id} не найдена");
            _dbSet.Remove(entity);
        }

        /// <summary>
        /// Проверить существование сущности по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        public virtual async Task<bool> ExistsAsync(TKey id)
        {
            return await _dbSet.AnyAsync(e => e.Id!.Equals(id));
        }
    }
}
