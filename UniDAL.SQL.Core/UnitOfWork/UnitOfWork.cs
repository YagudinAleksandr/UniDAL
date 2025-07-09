using Microsoft.EntityFrameworkCore;
using UniDAL.SQL.Abstractions;

namespace UniDAL.SQL.Core
{
    /// <summary>
    /// Реализация единицы работы (Unit of Work)
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        /// <inheritdoc cref="DbContext"/>
        protected readonly DbContext _context;

        /// <summary>
        /// Уничтожение объекта
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Контекст базы данных
        /// </summary>
        public DbContext Context => _context;

        /// <summary>
        /// Конструктор Unit of Work
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public UnitOfWork(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Получить репозиторий для работы с сущностью
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности</typeparam>
        /// <typeparam name="TKey">Тип идентификатора сущности</typeparam>
        public IRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : class, IEntity<TKey>
        {
            return new Repository<TEntity, TKey>(_context);
        }

        /// <summary>
        /// Подтвердить все изменения
        /// </summary>
        public virtual async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Откатить все изменения
        /// </summary>
        public virtual async Task RollbackAsync()
        {
            if (_context is IDatabaseContext databaseContext)
            {
                await databaseContext.RollbackAsync();
            }
        }

        /// <summary>
        /// Освободить ресурсы
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
}
