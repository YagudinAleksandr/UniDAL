using Microsoft.EntityFrameworkCore;
using UniDAL.SQL.Abstractions;

namespace UniDAL.SQL.Core
{
    /// <summary>
    /// Реализация фабрики для создания Unit of Work
    /// </summary>
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IContextFactory _contextFactory;

        /// <summary>
        /// Конструктор фабрики UoW
        /// </summary>
        /// <param name="contextFactory">Фабрика контекстов</param>
        public UnitOfWorkFactory(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        /// <summary>
        /// Создать Unit of Work для указанного контекста
        /// </summary>
        /// <typeparam name="TContext">Тип контекста</typeparam>
        /// <param name="contextKey">Ключ конфигурации</param>
        public IUnitOfWork Create<TContext>(string contextKey) where TContext : DbContext
        {
            var context = _contextFactory.Create<TContext>(contextKey);
            return new UnitOfWork(context);
        }
    }
}
