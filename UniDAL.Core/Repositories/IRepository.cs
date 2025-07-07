namespace UniDAL.Core
{
    /// <summary>
    /// Интерфейс базового репозитория
    /// </summary>
    /// <typeparam name="TEntity">Сущность</typeparam>
    /// <typeparam name="TKeyType">Тип ИД</typeparam>
    public interface IRepository<TEntity, TKeyType> where TEntity : class, IEntity<TKeyType>
    {
        /// <summary>
        /// Получение сущности по ИД
        /// </summary>
        /// <param name="id">ИД</param>
        /// <returns>Сущность</returns>
        Task<TEntity?> GetByIdAsync(TKeyType id);

        /// <summary>
        /// Добавление сущности
        /// </summary>
        /// <param name="entity">Сущность</param>
        Task InsertAsync(TEntity entity);

        /// <summary>
        /// Обновление сущности
        /// </summary>
        /// <param name="entity">Сущность</param>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <param name="id">ИД</param>
        Task DeleteAsync(TKeyType id);

        /// <summary>
        /// Получение сущности по запросу
        /// </summary>
        /// <returns><see cref="IQueryable"/> репозитория</returns>
        IQueryable<TEntity> Query();
    }
}
