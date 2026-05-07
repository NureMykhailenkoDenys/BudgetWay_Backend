namespace BudgetWay.Backend.DTOs.Dashboard;

public class DashboardDto
{
    public int TotalTrips { get; set; }

    public double TotalDistance { get; set; }

    public double TotalSpent { get; set; }

    public List<RecentTripDto> RecentTrips { get; set; }
        = new();
}