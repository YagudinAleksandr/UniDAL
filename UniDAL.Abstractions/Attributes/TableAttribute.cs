namespace UniDAL.Abstractions
{
    /// <summary>
    /// Указывает имя таблицы/коллекции в БД
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class TableAttribute : Attribute
    {
        /// <summary>
        /// Имя таблицы/коллекции
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Конструктор атрибута
        /// </summary>
        public TableAttribute(string name) => Name = name;
    }
}
