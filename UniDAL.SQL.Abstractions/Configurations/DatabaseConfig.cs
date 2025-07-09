namespace UniDAL.SQL.Abstractions
{
    /// <summary>
    /// Конфигурация подключения к конкретной базе данных
    /// </summary>
    public class DatabaseConfig
    {
        /// <summary>
        /// Тип СУБД (MSSQL, PostgreSQL, MySQL)
        /// </summary>
        public DatabaseProvider DbType { get; set; }

        /// <summary>
        /// Строка подключения к базе данных
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;
    }
}
