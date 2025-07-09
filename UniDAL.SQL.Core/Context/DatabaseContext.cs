using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using UniDAL.SQL.Abstractions;

namespace UniDAL.SQL.Core
{
    /// <summary>
    /// Базовый класс контекста базы данных с поддержкой транзакций
    /// </summary>
    public abstract class DatabaseContext : DbContext, IDatabaseContext
    {
        /// <inheritdoc cref="IDbContextTransaction"/>
        private IDbContextTransaction _transaction;

        /// <summary>
        /// Конструктор контекста
        /// </summary>
        /// <param name="options">Настройки контекста</param>
        protected DatabaseContext(DbContextOptions options) : base(options) { }

        /// <summary>
        /// Начать новую транзакцию
        /// </summary>
        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
        }

        /// <inheritdoc/>
        public async Task CommitAsync()
        {
            try
            {
                await SaveChangesAsync();
                _transaction?.Commit();
            }
            catch
            {
                _transaction?.Rollback();
                throw;
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        /// <inheritdoc/>
        public async Task RollbackAsync()
        {
            _transaction?.Rollback();
            await Task.CompletedTask;
        }
    }
}
