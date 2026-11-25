using System.ComponentModel.DataAnnotations;

namespace EmployeeDirectory.Api.DTOs;

public sealed class EmployeeCreateDto
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public int? DepartmentId { get; set; }
}
