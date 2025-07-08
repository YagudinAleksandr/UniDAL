using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace UniDAL.SQL.Core
{
    /// <summary>
    /// Абстрактный контекст подключения к БД
    /// </summary>
    public abstract class DatabaseContext : DbContext, IDisposable
    {
        /// <summary>
        /// Транзакция
        /// </summary>
        private IDbContextTransaction? _transaction;

        protected DatabaseContext(DbContextOptions options) : base(options) { }

        /// <summary>
        /// Начало транзакции
        /// </summary>
        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
        }

        /// <summary>
        /// Подтверждение транзакции
        /// </summary>
        public async Task Commit()
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

        /// <summary>
        /// Откат транзакции
        /// </summary>
        public async Task Rollback()
        {
            _transaction?.Rollback();
            await Task.CompletedTask;
        }
    }
}
