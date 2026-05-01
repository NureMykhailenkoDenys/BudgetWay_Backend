using BudgetWay.Backend.DTOs;
using BudgetWay.Backend.Models;
using BudgetWay.Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace BudgetWay.Backend.Services;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly JwtService _jwt;

    public AuthService(AppDbContext context, JwtService jwt)
    {
        _context = context;
        _jwt = jwt;
    }

    public async Task Register(RegisterDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            throw new Exception("User already exists");

        var user = new User
        {
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<string> Login(LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            throw new Exception("Invalid credentials");

        return _jwt.GenerateToken(user);
    }
}