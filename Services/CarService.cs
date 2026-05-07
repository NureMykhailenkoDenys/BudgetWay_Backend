using BudgetWay.Backend.Data;
using BudgetWay.Backend.DTOs.Cars;
using BudgetWay.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetWay.Backend.Services;

public class CarService
{
    private readonly AppDbContext _context;

    public CarService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CarResponseDto> CreateCar(int userId, CreateCarDto dto)
    {
        var car = new Car
        {
            UserId = userId,
            Name = dto.Name,
            FuelType = dto.FuelType,
            CityConsumption = dto.CityConsumption,
            HighwayConsumption = dto.HighwayConsumption
        };

        _context.Cars.Add(car);
        await _context.SaveChangesAsync();

        return new CarResponseDto
        {
            Id = car.Id,
            Name = car.Name,
            FuelType = car.FuelType,
            CityConsumption = car.CityConsumption,
            HighwayConsumption = car.HighwayConsumption
        };
    }

    public async Task<List<CarResponseDto>> GetUserCars(int userId)
    {
        return await _context.Cars
            .Where(c => c.UserId == userId)
            .Select(c => new CarResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                FuelType = c.FuelType,
                CityConsumption = c.CityConsumption,
                HighwayConsumption = c.HighwayConsumption
            })
            .ToListAsync();
    }

    public async Task<CarResponseDto?> UpdateCar(int userId, int carId, UpdateCarDto dto)
    {
        var car = await _context.Cars
            .FirstOrDefaultAsync(c => c.Id == carId && c.UserId == userId);

        if (car == null)
            return null;

        car.Name = dto.Name;
        car.FuelType = dto.FuelType;
        car.CityConsumption = dto.CityConsumption;
        car.HighwayConsumption = dto.HighwayConsumption;

        await _context.SaveChangesAsync();

        return new CarResponseDto
        {
            Id = car.Id,
            Name = car.Name,
            FuelType = car.FuelType,
            CityConsumption = car.CityConsumption,
            HighwayConsumption = car.HighwayConsumption
        };
    }

    public async Task<bool> DeleteCar(int userId, int carId)
    {
        var car = await _context.Cars
            .FirstOrDefaultAsync(c => c.Id == carId && c.UserId == userId);

        if (car == null)
            return false;

        _context.Cars.Remove(car);
        await _context.SaveChangesAsync();

        return true;
    }
}