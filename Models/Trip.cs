namespace BudgetWay.Backend.Models;

public class Trip
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public int CarId { get; set; }

    public string Name { get; set; } = string.Empty;

    public double DistanceKm { get; set; }
    public double FuelPrice { get; set; }

    public int CityPercentage { get; set; }
    public int HighwayPercentage { get; set; }

    public double FoodCost { get; set; }
    public double TollCost { get; set; }
    public double AccommodationCost { get; set; }
    public double OtherCost { get; set; }

    public double FuelCost { get; set; }
    public double TotalCost { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User? User { get; set; }
    public Car? Car { get; set; }
}