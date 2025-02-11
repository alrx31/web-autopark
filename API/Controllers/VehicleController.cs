using API.Exceptions;
using autopark.DAL.Entities;
using autopark.DAL.IRepository;
using autopark.DAL.Persistence.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class VehicleController(
    IVehicleRepository vehicleRepository
    ) : Controller
{
    // ReSharper disable once InconsistentNaming
    private const string VEHICLE_PROPERTIES =
        "VehicleId, VehicleTypeId, Model, RegistrationNumber, Weight, Year, Mileage, Color, FuelConsumption";

    public async Task<IActionResult> Index(int? criteria, int? direction)
    {
        var vehicles = await vehicleRepository.GetAllAsync();

        if (criteria.HasValue && direction.HasValue)
            vehicles = OrderVehicles(vehicles, criteria.Value, direction.Value);
        else
            vehicles = OrderVehicles(vehicles, 0); 
        
        ViewBag.CurrentCriteria = criteria ?? 0;
        ViewBag.CurrentDirection = direction ?? 0;

        return View(vehicles);
    }
    
    [HttpGet]
    public IActionResult Create() => View();
    
    [HttpPost]
    public async Task<IActionResult> Create([Bind(VEHICLE_PROPERTIES)] Vehicle vehicle)
    {
        if (!ModelState.IsValid) return View(vehicle);
        
        var resource = (await vehicleRepository.GetAllAsync()).Any(v=>v.Model == vehicle.Model && v.RegistrationNumber == vehicle.RegistrationNumber);
        if(resource) throw new AlreadyExistException("Vehicle already exists.");
        
        await vehicleRepository.AddAsync(vehicle);
        
        return RedirectToAction("Index");
    }

    private static IEnumerable<Vehicle> OrderVehicles(
        IEnumerable<Vehicle> vehicles,
        int criteria,
        int direction = 0)
    {
        return direction switch
        {
            0 => vehicles.OrderBy(v => v.GetPropertyByCriteria(criteria)),
            1 => vehicles.OrderByDescending(v => v.GetPropertyByCriteria(criteria)),
            _ => throw new ArgumentException("Invalid direction.")
        };
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var resource = await vehicleRepository.GetByIdAsync(id);
        if(resource is null) throw new NotFoundException("Vehicle not found.");
        
        return View(resource);
    }

    [HttpPost, ActionName("Update")]
    public async Task<IActionResult> UpdateConfirm(int id,[Bind(VEHICLE_PROPERTIES)] Vehicle vehicle)
    {
        if (!ModelState.IsValid) return View(vehicle);
        
        var resource = await vehicleRepository.GetByIdAsync(id);
        if(resource is null) throw new NotFoundException("Vehicle not found.");
        
        vehicle.VehicleId = id;
        await vehicleRepository.UpdateAsync(vehicle);   
        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var resource = await vehicleRepository.GetByIdAsync(id);
        if(resource is null) throw new NotFoundException("Vehicle not found.");

        return View(resource);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirm(int id)
    {
        var resource = await vehicleRepository.GetByIdAsync(id);
        if(resource is null) throw new NotFoundException("Vehicle not found.");
        
        await vehicleRepository.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}