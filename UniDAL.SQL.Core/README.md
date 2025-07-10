# UniDAL.SQL.Core

Реализация основных паттернов работы с базой данных (Repository, Unit of Work) для UniDAL на основе ADO.NET.

## Назначение

- Предоставляет базовые реализации репозиториев и Unit of Work.
- Использует абстракции из UniDAL.SQL.Abstractions.

## Использование

Добавьте зависимость в ваш проект:

```shell
dotnet add package UniDAL.SQL.Core
```

Пример регистрации в DI:

```csharp
services.AddUniDALSqlCore(options =>
{
    options.ConnectionString = "<ваша строка подключения>";
});
```

## Ссылки

- [Репозиторий на GitHub](https://github.com/YagudinAleksandr/UniDAL)
- [Документация UniDAL](https://github.com/YagudinAleksandr/UniDAL#readme) 