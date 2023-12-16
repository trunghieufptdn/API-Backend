using System.ComponentModel.DataAnnotations;

namespace WebApiLearn.Dtos.CarDtos;

public class CreateCarDtos
{
    [Required]
    public string Make { get; set; }

    [Required]
    public string Model { get; set; }

    [Required]
    public DateTime PublishDate { get; set; }
}