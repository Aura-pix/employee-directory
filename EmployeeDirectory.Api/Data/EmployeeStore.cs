using System.Collections.Concurrent;
using EmployeeDirectory.Api.Models;

namespace EmployeeDirectory.Api.Data;

public sealed class EmployeeStore
{
    private readonly ConcurrentDictionary<int, Employee> _employees = new();
    private int _nextId;

    public Employee Add(string firstName, string lastName, string email, int departmentId)
    {
        var id = Interlocked.Increment(ref _nextId);
        var employee = new Employee(id, firstName, lastName, email, departmentId);
        _employees.TryAdd(id, employee);
        return employee;
    }

    public bool TryGet(int id, out Employee? employee) =>
        _employees.TryGetValue(id, out employee);
}
