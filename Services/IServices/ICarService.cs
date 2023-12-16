using WebApiLearn.Dtos.CarDtos;
using WebApiLearn.Models;

namespace WebApiLearn.Services.IServices;

public interface ICarService
{
    Task<IEnumerable<Car>> GetAll();
    Task<Car> GetById(int id);
    Task Create(CreateCarDtos model);
    Task Update(int id, UpdateCarDtos model);
    Task Delete(int id);
}