# expense-tracker-expense-tracker-be
This is a simple financial tracker use to record own income and spending.

## Setup instructions
```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
cd ExpenseTracker
dotnet ef migrations add InitialCreate01
dotnet ef database update
```
- Change the sql connection string in appsettings.json.

## Design & architecture overview
- **Backend Framework**: .NET 8 Web API (chosen for strong performance and ecosystem support).
- **Database**: Microsoft SQL Server
- **Architecture**: 
  - Domain: Entities represent database table column.
  - DTOs: Request/Response objects.
  - Service: Bussiness logic or query logic implement the features.
  - Controller: Handle HTTP requests.
  - Interface: Define the service logic.
  - Infrastructure: 

## Trade-offs made
- **.Net**: .NET 8 was chosen for quicker development speed, better integration with EF Core, and simpler deployment on Windows/Linux.

## Areas for improvement and next steps for refactoring
- **Error Handling**: Add centralized exception handling middleware (instead of handling per-controller).
- **Validation**: Integrate FluentValidation or Data Annotations for stronger request validation.
- **Logging & Monitoring**: Introduce Serilog or NLog with structured logging for better observability.

## features were skipped due to time
- **Authentication**: Planned to implement JWT-based login for securing endpoints.
- **Data Encryption**: JSON payload encryption was skipped.

## Use of AI-assisted Tools
- **Gemini**: Translate SQL query to ef syntext.
- **Claude**: Implement date filter.