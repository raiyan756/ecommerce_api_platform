# Ecommerce API Platform

This is a small ASP.NET Core Web API for managing product categories. It uses Entity Framework Core with PostgreSQL, AutoMapper for DTO mapping, and provides paginated endpoints for categories.

## Prerequisites

- .NET 8 SDK: https://dotnet.microsoft.com/download
- PostgreSQL server (or adjust connection string for your DB)
- (Optional) `dotnet-ef` tool for applying migrations:

```powershell
dotnet tool install --global dotnet-ef
```

## Quick start (new PC)

1. Clone the repository:

```powershell
git clone <repo-url>
cd ecommerce_api_platform
```

2. Configure database connection

Open `appsettings.json` and set the `DefaultConnection` to point to your PostgreSQL instance. Example connection string:

```json
"DefaultConnection": "Host=localhost;Port=5432;Database=ecommerce_db;Username=postgres;Password=yourpassword"
```

3. Restore and build

```powershell
dotnet restore
dotnet build
```

4. Apply EF Core migrations

```powershell
dotnet ef database update
```

If you don't have `dotnet-ef` installed, run the install command from the Prerequisites section.

5. Run the API

```powershell
dotnet run
```

By default the app enables Swagger in the development environment: navigate to `https://localhost:<port>/swagger` to test endpoints.

## API Endpoints (Categories)

- GET `/api/categories` — list categories (supports query params `pageNumber`, `pageSize`, `search`, `sortOrder`)
- GET `/api/categories/{categoryId}` — get a single category by ID
- POST `/api/categories` — create category (body: `CreateCategoryDtos`)
- PUT `/api/categories/{categoryId}` — update category (body: `UpdateCategoryDtos`)
- DELETE `/api/categories/{categoryId}` — delete category

Example `curl` to create a category:

```bash
curl -X POST https://localhost:5001/api/categories \
  -H "Content-Type: application/json" \
  -d '{"categoryName":"Books","description":"All books"}'
```

## Project structure & key implementation

- `Controllers/CategoryControllers.cs`: API controller exposing category endpoints and using `ICategoryServices`.
- `Services/CategoryServices.cs`: Business logic, uses `AppDbContext` and `AutoMapper` to perform CRUD and paginated queries.
- `Data/AppDbContext.cs`: EF Core DbContext with `DbSet<Category> categories`.
- `Models/Categories.cs`: `Category` entity (Id, Name, Description, CreatedAt).
- `Dtos/`:
  - `CreateCategoryDtos.cs` — payload for creating a category
  - `UpdateCategoryDtos.cs` — payload for updating a category
  - `ReadCategortDtos.cs` — DTO returned to clients
- `Profile/CategoryProfile.cs`: AutoMapper profile mapping between entity and DTOs.
- `QueryHelper/QueryParameter.cs`: pagination, search and sort parameter validation.
- `Controllers/ApiResponses.cs` and `Controllers/Pagination.cs`: common response wrappers.

Design notes:
- Pagination and searching are implemented in `CategoryServices.GetAllCategories` using EF Core queries and `QueryParameter`.
- Sorting accepts named values (see `ParsedSortOrder.cs` / enum) and defaults to alphabetical by name.
- AutoMapper is registered in `Program.cs` and the `CategoryProfile` handles DTO mappings.
- Database provider: Npgsql (PostgreSQL). In `Program.cs` the app calls `UseNpgsql`.

## Troubleshooting

- Connection issues: verify PostgreSQL is running and the connection string in `appsettings.json` is correct.
- Missing `dotnet-ef`: install with `dotnet tool install --global dotnet-ef`.
- Migration errors: if you need to recreate the DB during development, you can drop the database and run `dotnet ef database update` again.

## Next steps / Improvements

- Add integration tests for endpoints
- Add more entities (Products, Orders, Users) and relationships
- Add seeding for initial data

---

If you want, I can also:
- commit the README to the repo and run a build
- add examples for Postman collection or Swagger export
