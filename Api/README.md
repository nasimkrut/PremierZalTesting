# PremierBank

---

## Архитектура

Проект построен по принципу чистой архитектуры:

```
Api                  // Внешний слой: контроллеры, Swagger, DI
Application          // Бизнес-логика, DTO, интерфейсы
Domain               // Сущности
Infrastructure       // EF Core, база, BankApiClient
```
---

## Реализовано:

* Импорт транзакций из BankApiClient
* Привязка к пользователям (по email)
* Поле `IsProcessed` для обработки
* EF Core + PostgreSQL + миграции
* Swagger UI (уже было)
* API:

  * `POST /api/transactions/import`
  * `GET /api/transactions/unprocessed`
  * `POST /api/transactions/"{id}/process"`
  * `GET /api/reports/monthly"`

* Раздробил сервисы и контроллеры для удобного расширения и читаемости 

---

## Как запустить

1. Создать PostgreSQL БД
2. Настроить `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=premierbank;Username=postgres;Password=your_password"
}
```

3. Выполнить миграцию:

```bash
cd Infrastructure
dotnet ef migrations add 'название миграции'
dotnet ef update database 
```

4. Запустить:

```bash
cd ../Api
dotnet run
```

5. Открыть Swagger: `https://localhost:5001/swagger`

---

## Обьяснение бага и придуманного решения
Решение работает на реальных данных, поэтому на моках я его закомментировал.
Суть бага в том что при добавлении одних и тех же транзакций, айди транзакций генерируется разный (случайный).
Решил написать метод на подобии генерации хэше, у одинаковых строк => одинаковый хэш.
Генерируем хэш на основе почты пользователя + времени добавления транзакции.
Нашел информацию, что UtcNow обновляются примерно каждые 10–15 мс.
Поэтому транзакции хэш в теории может быть одинаковым.
И это как раз таки повод для дальнейшего улучшения.

---

## Что ещё можно сделать?

* Отдельные сервисы для чтения/отчётов
* Юнит-тесты для сервисов
* Кеширование отчётов (повысить скорость)
* Логирование
* Docker/Docker Compose
* CI/CD (GitHub Actions)

