using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class ViewOrderViewModel
{
    [Range(0, int.MaxValue)]
    public required int OrderId { get; set; }
    [Range(0, int.MaxValue)]
    public required int VehicleId { get; init; }
    public required DateTime Date { get; set; }
    [StringLength(maximumLength: 100, ErrorMessage = "Name should be less than 100.")]
    public required string ComponentName { get; init; }
    [Range(1, int.MaxValue)]
    public required float Quantity { get; init; }
    [StringLength(maximumLength: 50, ErrorMessage = "Name should be less than 100.")]
    public required string VehicleModel { get; init; }
}