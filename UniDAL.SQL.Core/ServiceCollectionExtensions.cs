using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniDAL.SQL.Abstractions;
using UniDAL.SQL.Core.Factories;

namespace UniDAL.SQL.Core
{
    /// <summary>
    /// Методы расширения для регистрации сервисов в DI-контейнере
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавить сервисы работы с БД в DI-контейнер
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        /// <param name="configuration">Конфигурация приложения</param>
        public static IServiceCollection AddUnitDAL(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<DatabaseSettings>(config =>
                configuration.GetSection("SQLDatabases").Bind(config));

            // Регистрация фабрик
            services.AddScoped<IContextFactory, ContextFactory>();
            services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();

            return services;
        }
    }
}
