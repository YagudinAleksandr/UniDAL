using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UniDAL.SQL.Abstractions;
using UniDAL.SQL.Abstractions.Factories;

namespace UniDAL.SQL.Core.Factories
{
    /// <summary>
    /// Реализация фабрики для создания контекстов БД
    /// </summary>
    public class ContextFactory : IContextFactory
    {
        /// <inheritdoc cref="DatabaseSettings"/>
        private readonly DatabaseSettings _settings;

        /// <summary>
        /// Кэш контекстов
        /// </summary>
        private readonly Dictionary<string, DbContext> _contextCache = new();

        /// <summary>
        /// Конструктор фабрики контекстов
        /// </summary>
        /// <param name="settings">Настройки подключений</param>
        public ContextFactory(IOptions<DatabaseSettings> settings)
        {
            _settings = settings.Value;
        }

        /// <summary>
        /// Создать экземпляр контекста БД
        /// </summary>
        /// <typeparam name="TContext">Тип контекста</typeparam>
        /// <param name="contextKey">Ключ конфигурации</param>
        public TContext Create<TContext>(string contextKey) where TContext : DbContext
        {
            if (!_settings.SQLDatabases.TryGetValue(contextKey, out var config))
                throw new KeyNotFoundException($"Конфигурация для ключа '{contextKey}' не найдена");

            // Генерация уникального ключа кэша
            var cacheKey = $"{typeof(TContext).FullName}_{contextKey}";

            if (_contextCache.TryGetValue(cacheKey, out var cachedContext))
                return (TContext)cachedContext;

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            ConfigureProvider(optionsBuilder, config);

            var context = Activator.CreateInstance(
                typeof(TContext),
                optionsBuilder.Options) as TContext;

            _contextCache[cacheKey] = context;
            return context;
        }

        private void ConfigureProvider<TContext>(
            DbContextOptionsBuilder<TContext> builder,
            DatabaseConfig config) where TContext : DbContext
        {
            switch (config.DbType)
            {
                case DatabaseProvider.MSSQL:
                    builder.UseSqlServer(config.ConnectionString);
                    break;
                case DatabaseProvider.PostgreSQL:
                    builder.UseNpgsql(config.ConnectionString);
                    break;
                default:
                    throw new NotSupportedException($"Провайдер {config.DbType} не поддерживается");
            }
        }
    }
}
