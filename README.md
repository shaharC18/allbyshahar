# AllByShahar

An online ordering website built with ASP.NET Core 6 (Razor Pages).

## Features
- Menu with categories and item types
- Shopping cart and orders
- User accounts and roles (ASP.NET Core Identity)
- Payments with Stripe (test mode)
- Admin area for managing categories, items and orders

## Tech Stack
- C# / ASP.NET Core 6, Razor Pages
- Entity Framework Core with SQL Server
- ASP.NET Core Identity
- Stripe, MailKit
- GitHub Actions (build on every push and pull request)

## Project Structure
| Project | Purpose |
|---------|---------|
| `AllByShahar` | The website: pages, controllers, static files |
| `AllByShahar.DataAccess` | Database context, repositories, migrations |
| `AllByShahar.Models` | Data models |
| `AllByShahar.Utility` | Shared helpers: email, constants |

## Run locally
Requirements: .NET 6 SDK and SQL Server Express.

The Stripe secret key is not stored in the repository. Set it as an environment variable before running:
```
$env:Stripe__SecretKey = "<your Stripe test secret key>"
dotnet run --project AllByShahar
```
