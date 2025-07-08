namespace UniDAL.SQL.Abstractions
{
    /// <summary>
    /// Настройки подключения к базам данных
    /// </summary>
    public class DatabaseSettings
    {
        /// <summary>
        /// Подключение к БД
        /// </summary>
        public Dictionary<string, DatabaseConfig> SQLDatabases { get; set; } = new();
    }
}
