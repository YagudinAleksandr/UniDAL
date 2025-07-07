namespace UniDAL.Abstractions
{
    /// <summary>
    /// Базовый интерфейс репозитория с CRUD операциями
    /// </summary>
    public interface IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        /// <summary>
        /// Получение сущности по ИД
        /// </summary>
        /// <param name="id">ИД</param>
        /// <returns>Сущность</returns>
        TEntity? GetById(TKey id);

        /// <summary>
        /// Получение всех записей
        /// </summary>
        /// <returns>Все записи</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Создание записи
        /// </summary>
        /// <param name="entity">Сущность</param>
        Task Insert(TEntity entity);

        /// <summary>
        /// Обновление сущности
        /// </summary>
        /// <param name="entity">Сущность</param>
        Task Update(TEntity entity);

        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <param name="id">ИД</param>
        Task Delete(TKey id);
    }
}
