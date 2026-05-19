using BudgetWay.Backend.Data;
using BudgetWay.Backend.DTOs.Dashboard;
using Microsoft.EntityFrameworkCore;

namespace BudgetWay.Backend.Services;

public class DashboardService
{
    private readonly AppDbContext _context;

    public DashboardService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardDto> GetDashboard(int userId)
    {
        var trips = await _context.Trips
            .Where(t => t.UserId == userId)
            .ToListAsync();

        var recentTrips = trips
            .OrderByDescending(t => t.CreatedAt)
            .Take(5)
            .Select(t => new RecentTripDto
            {
                Id = t.Id,
                Name = t.Name,
                DistanceKm = t.DistanceKm,
                TotalCost = t.TotalCost,
                CreatedAt = t.CreatedAt
            })
            .ToList();

        return new DashboardDto
        {
            TotalTrips = trips.Count,

            TotalDistance = trips.Sum(t => t.DistanceKm),

            TotalSpent = trips.Sum(t => t.TotalCost),

            RecentTrips = recentTrips
        };
    }
}