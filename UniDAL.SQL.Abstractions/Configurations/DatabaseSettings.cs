namespace UniDAL.SQL.Abstractions
{
    /// <summary>
    /// Настройки всех SQL-подключений приложения
    /// </summary>
    public class DatabaseSettings
    {
        /// <summary>
        /// Словарь конфигураций баз данных
        /// </summary>
        /// <remarks>
        /// Ключ: Произвольное имя подключения (например: "Users", "Logs")
        /// Значение: Конфигурация конкретной БД
        /// </remarks>
        public Dictionary<string, DatabaseConfig> SQLDatabases { get; set; } = new();
    }
}
