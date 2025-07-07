namespace UniDAL.Core
{
    /// <summary>
    /// Интерфейс сущности
    /// </summary>
    /// <typeparam name="TKeyType">Тип ИД ключа</typeparam>
    public interface IEntity<TKeyType>
    {
        /// <summary>
        /// ИД
        /// </summary>
        TKeyType Id { get; set; }
    }
}
