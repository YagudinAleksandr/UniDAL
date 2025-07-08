namespace UniDAL.SQL.Abstractions
{
    /// <summary>
    /// Конфигурация базы данных
    /// </summary>
    public class DatabaseConfig
    {
        /// <summary>
        /// Провайдер БД
        /// </summary>
        public DatabaseProvider DbType { get; set; }

        /// <summary>
        /// Строка подключения
        /// </summary>
        public string ConnectionString { get; set; } = null!;
    }
}
