namespace UniDAL.Core
{
    /// <summary>
    /// Атрибут репозитория
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    internal class RepositoryAttribute : Attribute
    {
        /// <summary>
        /// Тип коннектора
        /// </summary>
        public DatabaseType DatabaseType { get; }

        /// <summary>
        /// Название таблицы
        /// </summary>
        public string? TableName { get; set; }

        public RepositoryAttribute(DatabaseType databaseType)
        {
            DatabaseType = databaseType;
        }
    }
}
