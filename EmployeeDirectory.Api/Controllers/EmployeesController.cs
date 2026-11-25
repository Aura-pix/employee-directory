using EmployeeDirectory.Api.Data;
using EmployeeDirectory.Api.DTOs;
using EmployeeDirectory.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDirectory.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class EmployeesController : ControllerBase
{
    private readonly EmployeeStore _employeeStore;

    public EmployeesController(EmployeeStore employeeStore)
    {
        _employeeStore = employeeStore;
    }

    [HttpPost]
    public ActionResult<Employee> Create(EmployeeCreateDto employeeDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var employee = _employeeStore.Add(
            employeeDto.FirstName,
            employeeDto.LastName,
            employeeDto.Email,
            employeeDto.DepartmentId!.Value);

        return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
    }

    [HttpGet("{id}")]
    public ActionResult<Employee> GetById(int id)
    {
        if (!_employeeStore.TryGet(id, out var employee))
        {
            return NotFound();
        }

        return Ok(employee);
    }
}
