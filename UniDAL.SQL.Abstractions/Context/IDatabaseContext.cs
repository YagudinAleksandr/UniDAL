namespace UniDAL.SQL.Abstractions
{
    // <summary>
    /// Интерфейс контекста базы данных с поддержкой транзакций
    /// </summary>
    public interface IDatabaseContext
    {
        /// <summary>
        /// Начать новую транзакцию
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Подтвердить транзакцию
        /// </summary>
        Task CommitAsync();

        /// <summary>
        /// Откатить транзакцию
        /// </summary>
        Task RollbackAsync();
    }
}
