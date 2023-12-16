using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiLearn.DbContext;
using WebApiLearn.Dtos.UserDtos;
using WebApiLearn.Models;
using WebApiLearn.Services.IServices;

namespace WebApiLearn.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _db;
    private readonly IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;

    public UserService(ApplicationDbContext db, IJwtUtils jwtUtils, IMapper mapper)
    {
        _db = db;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
    }

    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
    {
        var user = await _db.Users.FirstOrDefaultAsync(x => x.UserName == model.Username);
        
        // validate
        if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            throw new Exception("Username or Password is incorrect");
        
        // authenticate successful
        var response = _mapper.Map<AuthenticateResponse>(user);
        response.Token = _jwtUtils.GenerateToken(user);
        return response;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _db.Users.ToListAsync();
    }

    public async Task<User> GetById(int id)
    {
        return await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task Register(RegisterRequest model)
    {
        try
        {
            // validate
            var userList = await _db.Users.AnyAsync(x => x.UserName == model.Username);
            if (userList)
                throw new Exception("Username '" + model.Username + "' is already taken");

            // map model to new user object
            var user = _mapper.Map<User>(model);

            // hash password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // save user
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task Update(int id, UpdateRequest model)
    {
        try
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            
            //validate
            var userList = await _db.Users.AnyAsync(x => x.UserName == model.Username && x.Id != id);
            if (userList)
                throw new Exception("Username '" + model.Username + "' is already taken");
            
            // hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            
            // copy model to user and save
            _mapper.Map(model, user);

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
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) throw new KeyNotFoundException("User not found");
            _db.Users.Remove(user);
            _db.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}