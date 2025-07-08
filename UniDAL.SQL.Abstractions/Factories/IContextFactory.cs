using Microsoft.EntityFrameworkCore;

namespace UniDAL.SQL.Abstractions
{
    /// <summary>
    /// Фабрика контекстов
    /// </summary>
    public interface IContextFactory
    {
        /// <summary>
        /// Создание контекста
        /// </summary>
        /// <typeparam name="TContext">Контекст</typeparam>
        /// <param name="contextKey">Название контекста</param>
        /// <returns>Контекст</returns>
        TContext Create<TContext>(string contextKey) where TContext : DbContext;
    }
}
