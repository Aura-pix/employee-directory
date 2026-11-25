namespace EmployeeDirectory.Api.Models;

public sealed record Employee(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    int DepartmentId);
