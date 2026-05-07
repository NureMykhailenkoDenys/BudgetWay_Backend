using System.ComponentModel.DataAnnotations;

namespace BudgetWay.Backend.DTOs.Trips;

public class UpdateTripDto
{
    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [Range(1, 100000)]
    public double DistanceKm { get; set; }

    [Range(0.1, 1000)]
    public double FuelPrice { get; set; }

    [Range(0, 100)]
    public int CityPercentage { get; set; }

    [Range(0, 100000)]
    public double FoodCost { get; set; }

    [Range(0, 100000)]
    public double TollCost { get; set; }

    [Range(0, 100000)]
    public double AccommodationCost { get; set; }

    [Range(0, 100000)]
    public double OtherCost { get; set; }

    public int CarId { get; set; }
}