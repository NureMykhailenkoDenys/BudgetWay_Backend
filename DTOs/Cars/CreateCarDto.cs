using System.ComponentModel.DataAnnotations;

namespace BudgetWay.Backend.DTOs.Cars;

public class CreateCarDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    [RegularExpression("^(Gasoline|Diesel|Electric|Hybrid)$",ErrorMessage = "Invalid fuel type")]
    public string FuelType { get; set; } = string.Empty;

    [Range(0.1, 100)]
    public double CityConsumption { get; set; }

    [Range(0.1, 100)]
    public double HighwayConsumption { get; set; }
}