using System.ComponentModel.DataAnnotations;

namespace autopark.DAL.Entities;

public class Component
{
    [Range(0,int.MaxValue)]
    public required int ComponentId { get; set; }
    [StringLength(maximumLength: 100, ErrorMessage = "Name should be less than 100.")]
    public required string Name { get; init; }
}