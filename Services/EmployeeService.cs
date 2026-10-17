using EmployeeManagementApi.Data;
using EmployeeManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementApi.Services;

public class EmployeeService : IEmployeeService
{
    private readonly AppDbContext _context;

    public EmployeeService(AppDbContext context)
    {
        _context = context;
    }


    // get all
    public async Task<List<Employee>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Employees
            .AsNoTracking()
            .ToListAsync(ct);
    }


    // by id

    public async Task<Employee?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await _context.Employees
            .FirstOrDefaultAsync(e => e.Id == id, ct);
    }



    // create

    public async Task<Employee> CreateAsync(Employee employee, CancellationToken ct)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync(ct);

        return employee;
    }



    // updatee
    public async Task<bool> UpdateAsync(
        int id, Employee employee, CancellationToken ct)
    {
        var existing = await _context.Employees
            .FirstOrDefaultAsync(e => e.Id == id, ct);

        if (existing is null)
            return false;

        existing.Name = employee.Name;
        existing.Email = employee.Email;
        existing.Salary = employee.Salary;

        await _context.SaveChangesAsync(ct);
        return true;
    }




    // delete
    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var employee = await _context.Employees
            .FirstOrDefaultAsync(e => e.Id == id, ct);

        if (employee is null)
            return false;

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync(ct);

        return true;
    }
}