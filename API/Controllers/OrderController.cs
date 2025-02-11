using API.Exceptions;
using API.Models;
using autopark.DAL.Entities;
using autopark.DAL.IRepository;
using autopark.DAL.Persistence.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class OrderController(
    IOrderRepository orderRepository,
    IOrderItemRepository orderItemRepository,
    IVehicleRepository vehicleRepository,
    IComponentRepository componentRepository
    ) : Controller
{
    // ReSharper disable once InconsistentNaming
    private const string ORDER_PROPERTIES = "OrderId, VehicleId, Date";
    // ReSharper disable once InconsistentNaming
    private const string UNKNOWN_VALUE = "Unknown";
    
    public async Task<IActionResult> Index()
    {
        var orders = await orderRepository.GetAllAsync();
        var orderItems = (await orderItemRepository.GetAllAsync()).ToList(); 
        var components = (await componentRepository.GetAllAsync()).ToList();
        var vehicles = (await vehicleRepository.GetAllAsync()).ToList(); 

        var componentLookup = components.ToDictionary(c => c.ComponentId, c => c.Name);
        var vehicleLookup = vehicles.ToDictionary(v => v.VehicleId, v => v.Model);

        var res = 
            (from order in orders
        
                let items = orderItems.Where(o => o.OrderId == order.OrderId).ToList()
                from item in items
                let componentName = componentLookup.GetValueOrDefault(item.ComponentId, UNKNOWN_VALUE)
                let vehicleModel = vehicleLookup.GetValueOrDefault(order.VehicleId, UNKNOWN_VALUE)
                
                select new ViewOrderViewModel
                {
                    OrderId = order.OrderId,
                    VehicleId = order.VehicleId,
                    Date = order.Date,
                    ComponentName = componentName,
                    Quantity = item.Quantity,
                    VehicleModel = vehicleModel,
                }
            ).ToList();
        
        return View(res);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var model = new CreateOrderViewModel
        {
            VehicleId = 0,
            Components = [new OrderComponentViewModel
                {
                    Quantity = 0,
                    Id = 0
                }
            ]
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        
        var order = new Order
        {
            OrderId = 0,
            VehicleId = model.VehicleId,
            Date = DateTime.Now,
        };

        var id = await orderRepository.AddAsync(order);
        order.OrderId = id;
            
        foreach (var orderItem in model.Components.Select(component => new OrderItems
                 {
                     OrderId = order.OrderId,
                     ComponentId = component.Id,
                     Quantity= component.Quantity
                 }))
        {
            await orderItemRepository.AddAsync(orderItem);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var resource = await orderRepository.GetByIdAsync(id);
        if (resource is null) throw new NotFoundException("The resource was not found.");
        
        return View(resource);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirm(int id)
    {
        if (id < 1) return View();
        
        var resource = await orderRepository.GetByIdAsync(id);
        if (resource is null) throw new NotFoundException("The resource was not found.");
        
        await orderRepository.DeleteAsync(id);
        
        return RedirectToAction("Index");
    }
}