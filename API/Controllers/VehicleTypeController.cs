using API.Exceptions;
using autopark.DAL.Entities;
using autopark.DAL.IRepository;
using autopark.DAL.Persistence.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class VehicleTypeController(
    IVehicleTypeRepository vehicleRepository
    ): Controller
{
    // ReSharper disable once InconsistentNaming
    private const string VEHICLE_TYPE_PROPERTIES = "VehicleTypeId, Name, TaxCoefficient";
    public async Task<IActionResult> Index() => View(await vehicleRepository.GetAllAsync());

    [HttpGet]
    public IActionResult Create() => View();
    
    [HttpPost]
    public async Task<IActionResult> Create([Bind(VEHICLE_TYPE_PROPERTIES)] VehicleType vehicleType)
    {
        if (!ModelState.IsValid) return View(vehicleType);
        
        var resource = (await vehicleRepository.GetAllAsync()).Any(vy=>vy.Name==vehicleType.Name);
        if(resource) throw new AlreadyExistException("Vehicle Type already exists");
        
        await vehicleRepository.AddAsync(vehicleType);
        
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> Delete(int id)
    {
        var resource = await vehicleRepository.GetByIdAsync(id);
        if(resource is null) throw new NotFoundException("Resource not found");
        
        return View(resource);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirm(int vehicleTypeId)
    {
        var resource = await vehicleRepository.GetByIdAsync(vehicleTypeId);
        if(resource is null) throw new NotFoundException("Resource not found");
        
        await vehicleRepository.DeleteAsync(resource.VehicleTypeId);
        
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var resource = await vehicleRepository.GetByIdAsync(id);
        if(resource is null) throw new NotFoundException("Resource not found");
        
        return View(resource);
    }

    [HttpPost, ActionName("Update")]
    public async Task<IActionResult> UpdateConfirm(int id, [Bind(VEHICLE_TYPE_PROPERTIES)] VehicleType vehicleType)
    {
        if (!ModelState.IsValid) return View(vehicleType);
        
        var resource = await vehicleRepository.GetByIdAsync(id);
        if(resource is null) throw new NotFoundException("Resource not found");
        
        vehicleType.VehicleTypeId = id;
        await vehicleRepository.UpdateAsync(vehicleType);
        
        return RedirectToAction("Index");
    }
}