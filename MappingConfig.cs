using AutoMapper;
using WebApiLearn.Dtos;
using WebApiLearn.Dtos.CarDtos;
using WebApiLearn.Dtos.UserDtos;
using WebApiLearn.Models;

namespace WebApiLearn;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            // book
            config.CreateMap<UpSertBook, Book>().ReverseMap();
            // car
            config.CreateMap<UpSertCar, Car>().ReverseMap();
            config.CreateMap<UpdateCarDtos, Car>().ReverseMap();
            config.CreateMap<CreateCarDtos, Car>().ReverseMap();
            
            // mapping user
            config.CreateMap<User, AuthenticateResponse>();
            config.CreateMap<RegisterRequest, User>();
            config.CreateMap<UpdateRequest, User>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        return true;
                    }
                ));
        });
        return mappingConfig;
    }
        
}