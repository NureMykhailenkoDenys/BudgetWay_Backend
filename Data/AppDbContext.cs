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
    public DbSet<Car> Cars { get; set; }
    public DbSet<Trip> Trips { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User -> Cars
        modelBuilder.Entity<Car>()
            .HasOne(c => c.User)
            .WithMany(u => u.Cars)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // User -> Trips
        modelBuilder.Entity<Trip>()
            .HasOne(t => t.User)
            .WithMany(u => u.Trips)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Car -> Trips
        modelBuilder.Entity<Trip>()
            .HasOne(t => t.Car)
            .WithMany()
            .HasForeignKey(t => t.CarId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}