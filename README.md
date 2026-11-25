# Employee Directory API

A minimal ASP.NET Core Web API for creating and retrieving employees. Employee data is kept in memory and is lost when the application restarts.

## Endpoints

### Create an employee

`POST /api/employees`

Send the following JSON using the exact PascalCase property names shown:

```json
{
  "FirstName": "Ada",
  "LastName": "Lovelace",
  "Email": "ada@example.com",
  "DepartmentId": 7
}
```

On success, the API responds with **201 Created**, a `Location` header for the new employee, and a JSON body:

```json
{
  "Id": 1,
  "FirstName": "Ada",
  "LastName": "Lovelace",
  "Email": "ada@example.com",
  "DepartmentId": 7
}
```

Invalid or missing required values, or an invalid email address, return **400 Bad Request** with validation errors keyed by the exact DTO property names:

```json
{
  "FirstName": [
    "The FirstName field is required."
  ],
  "Email": [
    "The Email field is not a valid e-mail address."
  ],
  "DepartmentId": [
    "The DepartmentId field is required."
  ]
}
```

### Get an employee

`GET /api/employees/{id}`

Returns **200 OK** and the employee JSON when found, or **404 Not Found** when the ID does not exist.

## Run

From the repository root:

```sh
dotnet run --project EmployeeDirectory.Api
```

## Test

```sh
dotnet test EmployeeDirectory.Tests
```
