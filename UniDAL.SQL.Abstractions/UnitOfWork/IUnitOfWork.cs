namespace UniDAL.SQL.Abstractions
{
    /// <summary>
    /// Интерфейс единицы работы (Unit of Work)
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Получить репозиторий для работы с сущностью
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности</typeparam>
        /// <typeparam name="TKey">Тип идентификатора сущности</typeparam>
        IRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : class, IEntity<TKey>;

        /// <summary>
        /// Подтвердить все изменения в рамках транзакции
        /// </summary>
        Task CommitAsync();

        /// <summary>
        /// Откатить все изменения в рамках транзакции
        /// </summary>
        Task RollbackAsync();
    }
}
