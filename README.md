# UniDAL

## Описание
UniDAL.SQL предоставляет унифицированный интерфейс для работы с различными реляционными БД (PostgreSQL, SQL Server, MySQL) через Entity Framework Core. Библиотека состоит из двух пакетов:
- UniDAL.SQL.Abstractions - содержит интерфейсы и конфигурации
- UniDAL.SQL.Core - реализация функционала

### Установка UniDAL.SQL
```bash
dotnet add package UniDAL.SQL.Abstractions
dotnet add package UniDAL.SQL.Core
```

Или через NuGet Package Manager:

```bash
Install-Package UniDAL.SQL.Abstractions
Install-Package UniDAL.SQL.Core
```

#### Настройка
Добавьте конфигурацию в appsettings.json:

```json
{
  "SQLDatabases": {
    "MainDB": {
      "DbType": "PostgreSQL",
      "ConnectionString": "Host=localhost;Database=myapp;Username=user;Password=pass"
    },
    "LogDB": {
      "DbType": "MSSQL",
      "ConnectionString": "Server=localhost;Database=logs;User Id=sa;Password=your_password"
    }
  }
}
```

Зарегистрируйте сервисы в Startup.cs:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddUniDAL(Configuration);
    
    // Регистрация контекстов
    services.AddScoped<AppDbContext>();
    services.AddScoped<LogDbContext>();
}
```

##### Использование
1. Определение сущностей
```csharp
public class User : IEntity<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

public class Product : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
}
```

2. Создание контекстов
```csharp
public class AppDbContext : DatabaseContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
}
```

3. Использование в сервисах
```csharp
public class UserService
{
    private readonly IUnitOfWorkFactory _uowFactory;

    public UserService(IUnitOfWorkFactory uowFactory)
    {
        _uowFactory = uowFactory;
    }

    public async Task<int> CreateUser(UserDto dto)
    {
        using var uow = _uowFactory.Create<AppDbContext>("MainDB");
        var repo = uow.GetRepository<User, int>();
        
        var user = new User {
            Name = dto.Name,
            Email = dto.Email
        };
        
        await repo.AddAsync(user);
        await uow.CommitAsync();
        
        return user.Id;
    }
}
```

##### Примеры
Транзакции
```csharp
public async Task TransferFunds(int fromUserId, int toUserId, decimal amount)
{
    using var uow = _uowFactory.Create<AppDbContext>("MainDB");
    var userRepo = uow.GetRepository<User, int>();
    var accountRepo = uow.GetRepository<Account, int>();

    // Начинаем транзакцию
    (uow as UnitOfWork)?.Context.BeginTransaction();

    try
    {
        var fromAccount = await accountRepo.GetByIdAsync(fromUserId);
        var toAccount = await accountRepo.GetByIdAsync(toUserId);

        fromAccount.Balance -= amount;
        toAccount.Balance += amount;

        await accountRepo.UpdateAsync(fromAccount);
        await accountRepo.UpdateAsync(toAccount);

        await uow.CommitAsync();
    }
    catch
    {
        await uow.RollbackAsync();
        throw;
    }
}
```

Миграции
```bash
dotnet tool install --global dotnet-ef
```

Создайте миграцию:
```bash
dotnet ef migrations add Initial -c AppDbContext -o Migrations/MainDB
```

Примените миграции:
```bash
dotnet ef database update -c AppDbContext
```