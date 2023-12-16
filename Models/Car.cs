using System.ComponentModel.DataAnnotations;

namespace WebApiLearn.Models;

public class Car
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Make { get; set; }
    [Required]
    public string Model { get; set; }
    [Required]
    public DateTime PublishDate { get; set; }
}