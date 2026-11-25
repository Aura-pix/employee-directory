using System.Net;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace EmployeeDirectory.Tests;

public sealed class EmployeesApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public EmployeesApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Create_returns_employee_with_documented_property_names_and_get_retrieves_it()
    {
        using var response = await _client.PostAsync(
            "/api/employees",
            new StringContent(
                """{"FirstName":"Ada","LastName":"Lovelace","Email":"ada@example.com","DepartmentId":7}""",
                Encoding.UTF8,
                "application/json"));

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);
        using var createdJson = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var employee = createdJson.RootElement;
        var id = employee.GetProperty("Id").GetInt32();

        Assert.Equal("Ada", employee.GetProperty("FirstName").GetString());
        Assert.Equal("Lovelace", employee.GetProperty("LastName").GetString());
        Assert.Equal("ada@example.com", employee.GetProperty("Email").GetString());
        Assert.Equal(7, employee.GetProperty("DepartmentId").GetInt32());
        Assert.False(employee.TryGetProperty("firstName", out _));
        Assert.EndsWith(
            $"/api/employees/{id}",
            response.Headers.Location!.ToString(),
            StringComparison.OrdinalIgnoreCase);

        using var getResponse = await _client.GetAsync($"/api/employees/{id}");

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
    }

    [Fact]
    public async Task Create_returns_model_state_errors_for_invalid_data()
    {
        using var response = await _client.PostAsync(
            "/api/employees",
            new StringContent(
                """{"LastName":"Lovelace","Email":"not-an-email","DepartmentId":null}""",
                Encoding.UTF8,
                "application/json"));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        using var errorJson = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var errors = errorJson.RootElement;

        Assert.True(errors.TryGetProperty("FirstName", out _));
        Assert.True(errors.TryGetProperty("Email", out _));
        Assert.True(errors.TryGetProperty("DepartmentId", out _));
        Assert.Equal(
            "The Email field is not a valid e-mail address.",
            errors.GetProperty("Email")[0].GetString());
    }

    [Fact]
    public async Task Get_returns_not_found_for_unknown_employee()
    {
        using var response = await _client.GetAsync("/api/employees/9999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
