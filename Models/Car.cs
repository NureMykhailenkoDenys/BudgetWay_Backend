namespace BudgetWay.Backend.Models;

public class Car
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string FuelType { get; set; } = string.Empty;

    public double CityConsumption { get; set; }
    public double HighwayConsumption { get; set; }

    public User? User { get; set; }
}