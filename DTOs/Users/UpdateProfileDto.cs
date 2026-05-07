using System.ComponentModel.DataAnnotations;

namespace BudgetWay.Backend.DTOs.Users;

public class UpdateProfileDto
{
    [Required]
    [StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string? CurrentPassword { get; set; }

    [MinLength(6)]
    public string? NewPassword { get; set; }
}