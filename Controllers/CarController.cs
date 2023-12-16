using Microsoft.AspNetCore.Mvc;
using WebApiLearn.Authorization;
using AutoMapper;
using WebApiLearn.Dtos.CarDtos;
using WebApiLearn.Models;
using WebApiLearn.Services.IServices;

namespace WebApiLearn.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
//http://localhost/api/Book/
public class CarController : ControllerBase
{
    private ICarService _carService;
    private IMapper _mapper;

    public CarController(ICarService carService, IMapper mapper)
    {
        _carService = carService;
        _mapper = mapper;
    }

    [HttpGet]
    // GET http://localhost:5000/api/book
    public async Task<IActionResult> GetAll()
    {
        var allCars = await _carService.GetAll();
        return Ok(allCars);
    }

    [HttpGet("{id:int}")]
    // GET http://localhost:5000/api/book/5
    // id = 5
    public async Task<IActionResult> GetById(int id)
    {
        var car = await _carService.GetById(id);
        return Ok(car);
    }

    [Authorize(Role.Admin)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateCarDtos model)
    {
        await _carService.Create(model);
        return Ok();
    }

    [Authorize(Role.Admin)]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateCarDtos model)
    {
        await _carService.Update(id, model);
        return Ok(new { message = "Updated successfully" });
    }

    [Authorize(Role.Admin)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _carService.Delete(id);
        return Ok(new { message = "Deleted successfully" });
    }
}
