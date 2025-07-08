namespace UniDAL.SQL.Abstractions
{
    /// <summary>
    /// Базовый интерфейс UnitOfWork
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Получение репозитория
        /// </summary>
        /// <typeparam name="TEntity">Сущность</typeparam>
        /// <typeparam name="TKey">ИД</typeparam>
        /// <returns>Репозиторий <see cref="IRepository{TEntity, TKey}"/></returns>
        IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class, IEntity<TKey>;

        /// <summary>
        /// Создание коммита
        /// </summary>
        Task Commit();

        /// <summary>
        /// Откат изменений
        /// </summary>
        Task Rollback();
    }
}
