namespace BudgetWay.Backend.DTOs.Trips;

public class TripResponseDto
{
    public int Id { get; set; }

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

    public DateTime CreatedAt { get; set; }

    public int CarId { get; set; }
}