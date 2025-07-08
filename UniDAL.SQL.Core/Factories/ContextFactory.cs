using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UniDAL.SQL.Abstractions;

namespace UniDAL.SQL.Core
{
    /// <inheritdoc cref="IContextFactory"/>
    public class ContextFactory : IContextFactory
    {
        /// <inheritdoc cref="DatabaseSettings"/>
        private readonly DatabaseSettings _settings;

        /// <summary>
        /// Кэш контекстов (key - имя контекста, value - контекст)
        /// </summary>
        private readonly Dictionary<string, DbContext> _contextsCache = new();

        public ContextFactory(IOptions<DatabaseSettings> settings)
        {
            _settings = settings.Value;
        }

        /// <inheritdoc/>
        public TContext Create<TContext>(string contextKey) where TContext : DbContext
        {
            if (!_settings.SQLDatabases.TryGetValue(contextKey, out var config))
            {
                throw new KeyNotFoundException($"Database config for key '{contextKey}' not found");
            }

            if (_contextsCache.TryGetValue(contextKey, out var cachedContext))
            {
                return (TContext)cachedContext;
            }

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            ConfigureProvider(optionsBuilder, config);

            TContext? context = Activator.CreateInstance(typeof(TContext), optionsBuilder.Options) as TContext;

            if (context == null)
            {
                throw new NullReferenceException("Can not create database context");
            }

            _contextsCache[contextKey] = context;

            return context;
        }

        /// <summary>
        /// Формирование конфигурации контекста
        /// </summary>
        /// <typeparam name="TContext">Контекст</typeparam>
        /// <param name="builder">Построитель конфигурации</param>
        /// <param name="config">Конфигурация</param>
        /// <exception cref="NotSupportedException"></exception>
        private void ConfigureProvider<TContext>(
            DbContextOptionsBuilder<TContext> builder,
            DatabaseConfig config) where TContext : DbContext
        {
            switch (config.DbType)
            {
                case DatabaseProvider.SqlServer:
                    builder.UseSqlServer(config.ConnectionString);
                    break;
                case DatabaseProvider.PostgreSQL:
                    builder.UseNpgsql(config.ConnectionString);
                    break;
                default:
                    throw new NotSupportedException($"Database provider {config.DbType} is not supported");
            }
        }
    }
}
