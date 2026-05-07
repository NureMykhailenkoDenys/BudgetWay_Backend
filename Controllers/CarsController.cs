using System.Security.Claims;
using BudgetWay.Backend.DTOs.Cars;
using BudgetWay.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetWay.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CarsController : ControllerBase
{
    private readonly CarService _carService;

    public CarsController(CarService carService)
    {
        _carService = carService;
    }

    private int GetUserId()
    {
        return int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value
        );
    }

    [HttpPost]
    public async Task<IActionResult> CreateCar(CreateCarDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _carService.CreateCar(GetUserId(), dto);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserCars()
    {
        var cars = await _carService.GetUserCars(GetUserId());

        return Ok(cars);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCar(int id, UpdateCarDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _carService.UpdateCar(GetUserId(), id, dto);

        if (result == null)
            return NotFound("Car not found");

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCar(int id)
    {
        var deleted = await _carService.DeleteCar(GetUserId(), id);

        if (!deleted)
            return NotFound("Car not found");

        return Ok("Car deleted");
    }
}