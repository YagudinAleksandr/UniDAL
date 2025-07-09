namespace UniDAL.SQL.Abstractions
{
    /// <summary>
    /// Базовый интерфейс для всех сущностей
    /// </summary>
    /// <typeparam name="TKey">Тип идентификатора сущности</typeparam>
    public interface IEntity<TKeyType>
    {
        // <summary>
        /// Уникальный идентификатор сущности
        /// </summary>
        TKeyType Id { get; set; }
    }
}
