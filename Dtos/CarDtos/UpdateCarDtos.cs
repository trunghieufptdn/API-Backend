using System.ComponentModel.DataAnnotations;

namespace WebApiLearn.Dtos.CarDtos;

public class UpdateCarDtos
{
    public string Make { get; set; }
    public string Model { get; set; }
    public DateTime PublishDate  { get; set; }
    [Required]
    public string Password { get; set; }
}