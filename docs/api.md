# Employee Directory API

The API stores employee records in memory. JSON property names use PascalCase.

## Create an employee

`POST /api/employees`

Send a JSON object with all four required fields:

| Field | Type | Validation |
| --- | --- | --- |
| `FirstName` | string | Required |
| `LastName` | string | Required |
| `Email` | string | Required; must be a valid email address |
| `DepartmentId` | integer | Required |

Example request:

```json
{
  "FirstName": "Ada",
  "LastName": "Lovelace",
  "Email": "ada@example.com",
  "DepartmentId": 7
}
```

On success, the API returns **201 Created**, a `Location` header pointing to the new employee, and the created record as JSON:

```json
{
  "Id": 1,
  "FirstName": "Ada",
  "LastName": "Lovelace",
  "Email": "ada@example.com",
  "DepartmentId": 7
}
```

Invalid or missing required values, or an invalid email address, return **400 Bad Request**. The response is an object containing validation errors keyed by DTO field name. For example:

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

## Get an employee

`GET /api/employees/{id}`

Replace `{id}` with the employee's integer ID.

- **200 OK**: returns the matching employee record in the same JSON shape as the create response.
- **404 Not Found**: returned when no employee has that ID.
