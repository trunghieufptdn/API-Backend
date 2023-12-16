using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApiLearn.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName{ get; set; }
    public Role Role { get; set; }
    [JsonIgnore]
    public string PasswordHash { get; set; }
}