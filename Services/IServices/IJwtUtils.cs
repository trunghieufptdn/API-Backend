using WebApiLearn.Models;

namespace WebApiLearn.Services.IServices;

public interface IJwtUtils
{
    public string GenerateToken(User user);
    public int? ValidateToken(string token);
}