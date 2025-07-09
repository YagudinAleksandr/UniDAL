using Microsoft.EntityFrameworkCore;

namespace UniDAL.SQL.Abstractions
{
    /// <summary>
    /// Фабрика для создания экземпляров Unit of Work
    /// </summary>
    public interface IUnitOfWorkFactory
    {
        /// <summary>
        /// Создать Unit of Work для указанного контекста
        /// </summary>
        /// <typeparam name="TContext">Тип контекста БД</typeparam>
        /// <param name="contextKey">Ключ конфигурации из настроек</param>
        IUnitOfWork Create<TContext>(string contextKey) where TContext : DbContext;
    }
}
