namespace UniDAL.SQL.Abstractions
{
    /// <summary>
    /// Базовый интерфейс репозитория
    /// </summary>
    /// <typeparam name="TEntity">Сущность</typeparam>
    /// <typeparam name="TKey">Ключ</typeparam>
    public interface IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        /// <summary>
        /// Получение сущности по ИД
        /// </summary>
        /// <param name="id">ИД</param>
        /// <returns>Сущность</returns>
        Task<TEntity?> GetByIdAsync(TKey id);

        /// <summary>
        /// Получение всех сущностей
        /// </summary>
        /// <returns>Список сущностей</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Добавление сущности
        /// </summary>
        /// <param name="entity">Сущность</param>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Обновление сущности
        /// </summary>
        /// <param name="entity">Сущность</param>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <param name="id">ИД</param>
        Task DeleteAsync(TKey id);
    }
}
