using BudgetWay.Backend.Data;
using BudgetWay.Backend.DTOs.Trips;
using BudgetWay.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetWay.Backend.Services;

public class TripService
{
    private readonly AppDbContext _context;

    public TripService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TripResponseDto?> CreateTrip(int userId, CreateTripDto dto)
    {
        var car = await _context.Cars
            .FirstOrDefaultAsync(c => c.Id == dto.CarId && c.UserId == userId);

        if (car == null)
            return null;

        var highwayPercentage = 100 - dto.CityPercentage;

        var cityDistance = dto.DistanceKm * dto.CityPercentage / 100.0;
        var highwayDistance = dto.DistanceKm * highwayPercentage / 100.0;

        var fuelUsed =
            (cityDistance * car.CityConsumption / 100.0) +
            (highwayDistance * car.HighwayConsumption / 100.0);

        var fuelCost = fuelUsed * dto.FuelPrice;

        var totalCost =
            fuelCost +
            dto.FoodCost +
            dto.TollCost +
            dto.AccommodationCost +
            dto.OtherCost;

        var trip = new Trip
        {
            UserId = userId,
            CarId = dto.CarId,

            Name = dto.Name,

            DistanceKm = dto.DistanceKm,
            FuelPrice = dto.FuelPrice,

            CityPercentage = dto.CityPercentage,
            HighwayPercentage = highwayPercentage,

            FoodCost = dto.FoodCost,
            TollCost = dto.TollCost,
            AccommodationCost = dto.AccommodationCost,
            OtherCost = dto.OtherCost,

            FuelCost = fuelCost,
            TotalCost = totalCost
        };

        _context.Trips.Add(trip);

        await _context.SaveChangesAsync();

        return MapTrip(trip);
    }

    public async Task<List<TripResponseDto>> GetUserTrips(int userId)
    {
        return await _context.Trips
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => MapTrip(t))
            .ToListAsync();
    }

    public async Task<TripResponseDto?> GetTripById(int userId, int tripId)
    {
        var trip = await _context.Trips
            .FirstOrDefaultAsync(t => t.Id == tripId && t.UserId == userId);

        if (trip == null)
            return null;

        return MapTrip(trip);
    }

    public async Task<TripResponseDto?> UpdateTrip(int userId, int tripId, UpdateTripDto dto)
    {
        var trip = await _context.Trips
            .FirstOrDefaultAsync(t => t.Id == tripId && t.UserId == userId);

        if (trip == null)
            return null;

        var car = await _context.Cars
            .FirstOrDefaultAsync(c => c.Id == dto.CarId && c.UserId == userId);

        if (car == null)
            return null;

        var highwayPercentage = 100 - dto.CityPercentage;

        var cityDistance = dto.DistanceKm * dto.CityPercentage / 100.0;
        var highwayDistance = dto.DistanceKm * highwayPercentage / 100.0;

        var fuelUsed =
            (cityDistance * car.CityConsumption / 100.0) +
            (highwayDistance * car.HighwayConsumption / 100.0);

        var fuelCost = fuelUsed * dto.FuelPrice;

        var totalCost =
            fuelCost +
            dto.FoodCost +
            dto.TollCost +
            dto.AccommodationCost +
            dto.OtherCost;

        trip.CarId = dto.CarId;

        trip.Name = dto.Name;

        trip.DistanceKm = dto.DistanceKm;
        trip.FuelPrice = dto.FuelPrice;

        trip.CityPercentage = dto.CityPercentage;
        trip.HighwayPercentage = highwayPercentage;

        trip.FoodCost = dto.FoodCost;
        trip.TollCost = dto.TollCost;
        trip.AccommodationCost = dto.AccommodationCost;
        trip.OtherCost = dto.OtherCost;

        trip.FuelCost = fuelCost;
        trip.TotalCost = totalCost;

        await _context.SaveChangesAsync();

        return MapTrip(trip);
    }

    public async Task<bool> DeleteTrip(int userId, int tripId)
    {
        var trip = await _context.Trips
            .FirstOrDefaultAsync(t => t.Id == tripId && t.UserId == userId);

        if (trip == null)
            return false;

        _context.Trips.Remove(trip);

        await _context.SaveChangesAsync();

        return true;
    }

    private static TripResponseDto MapTrip(Trip trip)
    {
        return new TripResponseDto
        {
            Id = trip.Id,
            Name = trip.Name,

            DistanceKm = trip.DistanceKm,
            FuelPrice = trip.FuelPrice,

            CityPercentage = trip.CityPercentage,
            HighwayPercentage = trip.HighwayPercentage,

            FoodCost = trip.FoodCost,
            TollCost = trip.TollCost,
            AccommodationCost = trip.AccommodationCost,
            OtherCost = trip.OtherCost,

            FuelCost = trip.FuelCost,
            TotalCost = trip.TotalCost,

            CreatedAt = trip.CreatedAt,

            CarId = trip.CarId
        };
    }
}