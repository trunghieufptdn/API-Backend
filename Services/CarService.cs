using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiLearn.Services.IServices;
using WebApiLearn.DbContext;
using WebApiLearn.Dtos.CarDtos;
using WebApiLearn.Models;

namespace WebApiLearn.Services;

public class CarService : ICarService
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CarService(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Car>> GetAll()
    {
        return await _db.Cars.ToListAsync();
    }

    public async Task<Car> GetById(int id)
    {
        return await _db.Cars.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task Create(CreateCarDtos model)
    {
        try
        {
            var car = _mapper.Map<Car>(model);

            await _db.Cars.AddAsync(car);
            await _db.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task Update(int id, UpdateCarDtos model)
    {
        try
        {
            var car = await _db.Cars.FirstOrDefaultAsync(c => c.Id == id);
            if (car == null)
            {
                throw new KeyNotFoundException("Car not found");
            }

            _mapper.Map(model, car);

            await _db.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task Delete(int id)
    {
        try
        {
            var car = await _db.Cars.FirstOrDefaultAsync(c => c.Id == id);
            if (car == null)
            {
                throw new KeyNotFoundException("Car not found");
            }

            _db.Cars.Remove(car);
            await _db.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}