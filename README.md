# Employee Directory API

A minimal ASP.NET Core Web API for creating and retrieving employee records. It targets .NET 10 and keeps data in memory, so records are cleared when the application restarts.

This repository is a test project for documentation tooling. The API contract is documented in [docs/api.md](docs/api.md).

## Run

From the repository root:

```sh
dotnet run --project EmployeeDirectory.Api
```

Run the API tests with:

```sh
dotnet test EmployeeDirectory.Tests
```
