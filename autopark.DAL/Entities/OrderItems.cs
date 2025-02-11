using System.ComponentModel.DataAnnotations;

namespace autopark.DAL.Entities;

public class OrderItems
{
    [Range(0, int.MaxValue)]
    public int OrderItemId { get; set; }
    [Range(1, int.MaxValue)]
    public int OrderId { get; set; }
    [Range(0, int.MaxValue)]
    public int ComponentId { get; set; }
    [Range(0, int.MaxValue)]
    public float Quantity { get; set; }
    
    public Order? Order { get; set; }
    public Component? Component { get; set; }
    
}