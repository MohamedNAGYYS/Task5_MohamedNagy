using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementApi.Models;

public class Employee
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Range(0, 10000000)]
    public decimal Salary { get; set; }
}