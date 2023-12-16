using WebApiLearn.Models;

namespace WebApiLearn.Dtos.UserDtos;

public class AuthenticateResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public Role Role { get; set; }
    public string Token { get; set; }
}