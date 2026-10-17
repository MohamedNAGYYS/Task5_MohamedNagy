using EmployeeManagementApi.Models;
using EmployeeManagementApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeesController(IEmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Employee>>> GetAll(
        CancellationToken ct)
    {
        return Ok(await _service.GetAllAsync(ct));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Employee>> GetById(
        int id, CancellationToken ct)
    {
        var employee = await _service.GetByIdAsync(id, ct);

        if (employee is null)
            return NotFound();

        return Ok(employee);
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> Create(
        Employee employee, CancellationToken ct)
    {
        var created = await _service.CreateAsync(employee, ct);

        return CreatedAtAction(
            nameof(GetById),
            new { id = created.Id },
            created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id, Employee employee, CancellationToken ct)
    {
        var updated = await _service.UpdateAsync(id, employee, ct);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id, CancellationToken ct)
    {
        var deleted = await _service.DeleteAsync(id, ct);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}