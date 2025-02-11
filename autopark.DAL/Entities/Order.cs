using System.ComponentModel.DataAnnotations;

namespace autopark.DAL.Entities;

public class Order
{
    [Range(0, int.MaxValue)]
    public required int OrderId { get; set; }
    [Range(0, int.MaxValue)]
    public required int VehicleId { get; init; }
    public required DateTime Date { get; set; }
}