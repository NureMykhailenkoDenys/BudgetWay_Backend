using BudgetWay.Backend.Models;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;
using BudgetWay.Backend.Models;

namespace BudgetWay.Backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}