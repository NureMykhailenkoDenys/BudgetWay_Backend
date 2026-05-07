using BudgetWay.Backend.Data;
using BudgetWay.Backend.DTOs.Users;
using Microsoft.EntityFrameworkCore;

namespace BudgetWay.Backend.Services;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserProfileDto?> GetProfile(int userId)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return null;

        return new UserProfileDto
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }

    public async Task<(bool Success, string Message)> UpdateProfile(
        int userId,
        UpdateProfileDto dto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return (false, "User not found");

        var emailExists = await _context.Users
            .AnyAsync(u => u.Email == dto.Email && u.Id != userId);

        if (emailExists)
            return (false, "Email already in use");

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.Email = dto.Email;

        if (!string.IsNullOrWhiteSpace(dto.NewPassword))
        {
            if (string.IsNullOrWhiteSpace(dto.CurrentPassword))
                return (false, "Current password is required");

            var validPassword = BCrypt.Net.BCrypt.Verify(
                dto.CurrentPassword,
                user.PasswordHash
            );

            if (!validPassword)
                return (false, "Current password is incorrect");

            user.PasswordHash =
                BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
        }

        await _context.SaveChangesAsync();

        return (true, "Profile updated");
    }
}