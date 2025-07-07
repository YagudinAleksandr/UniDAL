namespace UniDAL.Abstractions
{
    /// <summary>
    /// Базовый интерфейс сущности
    /// </summary>
    /// <typeparam name="TKeyType">Тип ИД</typeparam>
    public interface IEntity<TKeyType>
    {
        /// <summary>
        /// ИД
        /// </summary>
        TKeyType Id { get; set; }
    }
}
