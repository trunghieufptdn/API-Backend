using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiLearn.Authorization;
using WebApiLearn.Dtos.UserDtos;
using WebApiLearn.Models;
using WebApiLearn.Services.IServices;

namespace WebApiLearn.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private IUserService _userService;
    private IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest model)
    {
        var response = await _userService.Authenticate(model);
        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest model)
    {
        await _userService.Register(model);
        return Ok(new { message = "Registration successful" });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAll();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetById(id);
        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateRequest model)
    {
        await _userService.Update(id, model);
        return Ok(new { message = "User updated successfully" });
    }
    
    [Authorize(Role.Admin)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _userService.Delete(id);
        return Ok(new { message = "User deleted successfully" });
    }
}