# expense-tracker-expense-tracker-be
This is a simple financial tracker use to record own income and spending.

# Nuget Required
```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

# Migration
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```
