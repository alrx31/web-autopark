namespace API.Models;

public class CreateOrderViewModel
{
    public required int VehicleId { get; set; }

    public List<OrderComponentViewModel> Components { get; set; } = new List<OrderComponentViewModel>();   
}