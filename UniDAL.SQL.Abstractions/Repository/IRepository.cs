namespace UniDAL.SQL.Abstractions
{
    /// <summary>
    /// Базовый интерфейс репозитория для CRUD-операций
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    /// <typeparam name="TKey">Тип идентификатора сущности</typeparam>
    public interface IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        /// <summary>
        /// Получить сущность по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        Task<TEntity?> GetByIdAsync(TKey id);

        /// <summary>
        /// Получить все сущности
        /// </summary>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Добавить новую сущность
        /// </summary>
        /// <param name="entity">Добавляемая сущность</param>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Обновить существующую сущность
        /// </summary>
        /// <param name="entity">Сущность для обновления</param>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Удалить сущность по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        Task DeleteAsync(TKey id);

        /// <summary>
        /// Проверить существование сущности по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        Task<bool> ExistsAsync(TKey id);
    }
}
