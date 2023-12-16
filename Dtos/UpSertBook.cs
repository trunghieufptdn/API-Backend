using System.ComponentModel.DataAnnotations;

namespace WebApiLearn.Dtos;

public class UpSertBook
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Author { get; set; }
    [Required]
    public double Price { get; set; }
    [Required]
    public DateTime PublishDate { get; set; }
}