namespace BudgetWay.Backend.DTOs.Dashboard;

public class RecentTripDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public double DistanceKm { get; set; }

    public double TotalCost { get; set; }

    public DateTime CreatedAt { get; set; }
}