  # WordCounter API with Clean Architecture

## Getting Started
1. Navigate to `src/Apps/WordCounter` and run `dotnet run` to launch the back end (ASP.NET Core Web API)
2. Open web browser https://localhost:44372/swagger Swagger UI
3. Navigate to "api/login" and Pass Request Param Email = `raghav@test.com` and Password = `Raghav@123` after that copy token
4. Nagigate to Authorize page and add validation token with `Bearer [token]`
Example : `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9`


### Database Configuration

The API configured to use an in-memory database by default. This ensures that all users will be able to run the solution without needing to set up additional infrastructure (e.g. SQL Server).

If you would like to use SQL Server, you will need to update **WebApi/appsettings.json** as follows:

```json
  "UseInMemoryDatabase": false,
```

Verify that the **DefaultConnection** connection string within **appsettings.json** points to a valid SQL Server instance. 

When you run the application the database will be automatically created (if necessary) and the latest migrations will be applied.

### Database Migrations

To use `dotnet-ef` for your migrations please add the following flags to your command (values assume you are executing from repository root)

* `--project src/Common/WordCounter.Infrastructure` (optional if in this folder)
* `--startup-project src/Apps/WordCounter`
* `--output-dir Persistence/Migrations`

For example, to add a new migration from the root folder:

 `dotnet ef migrations add "CreateDb" --project src\Common\WordCounter.Infrastructure --startup-project src\Apps\WordCounter --output-dir Persistence\Migrations`

 `dotnet ef database update --project src\Common\WordCounter.Infrastructure  --startup-project src\Apps\WordCounter`

If you getting Command dotnet ef not found error message then run: 
```
dotnet tool install --global dotnet-ef
```

## Overview

### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

### Infrastructure

This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### Api

This layer is a web api application based on .NET Core 3.1.x. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only *Startup.cs* should reference Infrastructure.

### Logs

Logging using Serilog and viewing logs in Console.

## License

This project is licensed with the [MIT license](LICENSE).
