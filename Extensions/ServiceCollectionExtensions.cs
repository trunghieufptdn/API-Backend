using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApiLearn.DbContext;
using WebApiLearn.Services;
using WebApiLearn.Services.IServices;

namespace WebApiLearn.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection service)
    {
        //Configure DbContext with Scoped Lifetime
        service.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(AppSettings.ConnectionStrings));
        
        return service;
    } 
    
    public static IServiceCollection AddCORS(this IServiceCollection service)
    {
        service.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder =>
                {
                    builder
                        .WithOrigins(AppSettings.CORS)
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .AllowAnyHeader();
                });
        });

        return service;
    }
    
    public static IServiceCollection AddAutoMapper(this IServiceCollection service)
    {
        //auto mapper config
        var mapper = MappingConfig.RegisterMaps().CreateMapper();
        service.AddSingleton(mapper);
        service.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return service;
    }

    public static IServiceCollection AddServices(this IServiceCollection service)
    {
        service.AddScoped<IJwtUtils, JwtUtils>();
        service.AddScoped<IUserService, UserService>();
        service.AddScoped<ICarService, CarService>();

        return service;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection service)
    {
        service.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web api", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description =
                    "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
                    "Enter 'Bearer'[space] and then your token in the text input below.\r\n\r\n" +
                    "Example: \"Bearer 12345abcdef\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        });
        return service;
    }
}