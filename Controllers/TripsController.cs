using System.Security.Claims;
using BudgetWay.Backend.DTOs.Trips;
using BudgetWay.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetWay.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TripsController : ControllerBase
{
    private readonly TripService _tripService;

    public TripsController(TripService tripService)
    {
        _tripService = tripService;
    }

    private int GetUserId()
    {
        return int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value
        );
    }

    [HttpPost]
    public async Task<IActionResult> CreateTrip(CreateTripDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _tripService.CreateTrip(GetUserId(), dto);

        if (result == null)
            return BadRequest("Car not found");

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetTrips()
    {
        var trips = await _tripService.GetUserTrips(GetUserId());

        return Ok(trips);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTrip(int id)
    {
        var trip = await _tripService.GetTripById(GetUserId(), id);

        if (trip == null)
            return NotFound("Trip not found");

        return Ok(trip);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTrip(int id, UpdateTripDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _tripService.UpdateTrip(GetUserId(), id, dto);

        if (result == null)
            return NotFound("Trip or Car not found");

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTrip(int id)
    {
        var deleted = await _tripService.DeleteTrip(GetUserId(), id);

        if (!deleted)
            return NotFound("Trip not found");

        return Ok("Trip deleted");
    }
}