namespace BudgetWay.Backend.DTOs.Cars;

public class CarResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string FuelType { get; set; } = string.Empty;

    public double CityConsumption { get; set; }

    public double HighwayConsumption { get; set; }
}