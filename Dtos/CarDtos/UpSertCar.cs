using System.ComponentModel.DataAnnotations;

namespace WebApiLearn.Dtos;

public class UpSertCar
{
    [Required]
    public string Make { get; set; }
    [Required]
    public string Model { get; set; }
    [Required]
    public DateTime PublishDate { get; set; }
}