using Microsoft.EntityFrameworkCore;

namespace UniDAL.SQL.Abstractions.Factories
{
    /// <summary>
    /// Фабрика для создания экземпляров контекста БД
    /// </summary>
    public interface IContextFactory
    {
        /// <summary>
        /// Создать экземпляр контекста БД
        /// </summary>
        /// <typeparam name="TContext">Тип контекста</typeparam>
        /// <param name="contextKey">Ключ конфигурации из настроек</param>
        TContext Create<TContext>(string contextKey) where TContext : DbContext;
    }
}
