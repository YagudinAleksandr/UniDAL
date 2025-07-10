# UniDAL.SQL.Abstractions

Библиотека содержит абстракции для работы с базой данных в UniDAL: интерфейсы репозиториев, Unit of Work, контекста и базовых сущностей.

## Назначение

- Определяет основные интерфейсы для реализации паттернов Repository и Unit of Work.
- Используется как контракт для реализации конкретных провайдеров БД.

## Использование

Добавьте зависимость в ваш проект:

```shell
dotnet add package UniDAL.SQL.Abstractions
```

Реализуйте свои репозитории и Unit of Work, наследуясь от интерфейсов из этого пакета.

## Ссылки

- [Репозиторий на GitHub](https://github.com/YagudinAleksandr/UniDAL)
- [Документация UniDAL](https://github.com/YagudinAleksandr/UniDAL#readme) 