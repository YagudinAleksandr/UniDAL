using Microsoft.EntityFrameworkCore;
using UniDAL.SQL.Abstractions;

namespace UniDAL.SQL.Core
{
    /// <inheritdoc cref="IUnitOfWork"/>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IContextFactory _contextFactory;

        public UnitOfWork(IContextFactory contextFactory) 
        {
            _contextFactory = contextFactory;
        }

        public IUnitOfWork Create<TContext>(string contextKey) where TContext : DbContext
        {
            var context = _contextFactory.Create<TContext>(contextKey);
            return new UnitOfWork(context);
        }
    }
}
