# Content Order System

## Project Overview
This is a console-based application for managing and ordering content, including articles and subscription packages. 
It demonstrates object-oriented design principles with a clean architecture structure (Domain, Application, Infrastructure, Web/Console layers). 
The application supports CRUD operations, purchasing multiple items at once, and SMS notifications for order status.

## Features
- View, add, edit, and delete articles
- View, add, edit, and delete subscription packages
- Place orders with multiple items and subscriptions
- Validate input data
- Handle expected and unexpected errors
- SMS notifications with provider failover in case of rate limits

## Architecture
- **Domain Layer**: Entities (`Article`, `SubscriptionPackage`, `Order`) and `OrderStatus` enum  
- **Application Layer**: Services for business logic (`ArticleService`, `SubscriptionService`, `OrderService`)  
- **Infrastructure Layer**: In-memory repositories, SMS providers, `InMemoryUnitOfWork`  
- **Web/Console Layer**: Controllers (`ArticlesController`, `OrdersController`) and menu-driven console app  

## Project Structure
```
ContentOrderSystem/
│
├─ Domain/
│   └─ Entities and enums
├─ Application/
│   ├─ Interfaces
│   └─ Services
├─ Infrastructure/
│   ├─ Repositories
│   ├─ Sms
│   └─ UnitOfWork
├─ Web/
│   ├─ Controllers
│   └─ ConsoleApp.cs
└─ SQL_Schema.sql
```

## Notes
- This version uses in-memory repositories. No real database is required.
- SMS notifications are simulated via `SmsGateway` with provider failover logic.

## SQL Schema
- Tables: `Articles`, `SubscriptionPackages`, `Orders`, `OrderArticles`, `OrderSubscription`, `CustomerPurchases`  
- Soft delete implemented using `IsArchived` fields
- `OrderStatus` enforced via `CHECK` constraint