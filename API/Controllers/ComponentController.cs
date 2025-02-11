using API.Exceptions;
using autopark.DAL.Entities;
using autopark.DAL.IRepository;
using autopark.DAL.Persistence.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ComponentController(
    IComponentRepository componentRepository
    ) : Controller
{
    // ReSharper disable once InconsistentNaming
    private const string COMPONENT_PROPERTIES = "ComponentId, Name";

    public async Task<IActionResult> Index()
    {
        return View(await componentRepository.GetAllAsync());
    }
    
    [HttpGet]
    public IActionResult Create() => View();
    
    [HttpPost]
    public async Task<IActionResult> Create([Bind(COMPONENT_PROPERTIES)] Component component)
    {
        if (!ModelState.IsValid) return View(component);
        
        var exist = (await componentRepository.GetAllAsync()).Any(c => c.Name == component.Name);
        if (exist) throw new AlreadyExistException($"Component {component.Name} already exists.");
        
        await componentRepository.AddAsync(component);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var resource = await componentRepository.GetByIdAsync(id);
        if(resource is null) throw new NotFoundException("The resource was not found.");
        
        return View(resource);
    }

    [HttpPost, ActionName("Update")]
    public async Task<IActionResult> UpdateConfirm(int id, [Bind(COMPONENT_PROPERTIES)] Component component)
    {
        component.ComponentId = id;
        
        if (!ModelState.IsValid) return View(component);
        var exist = (await componentRepository.GetAllAsync()).Any(c => c.Name == component.Name);
        if (exist) throw new AlreadyExistException($"Component {component.Name} already exists.");
        
        await componentRepository.UpdateAsync(component);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var resource = await componentRepository.GetByIdAsync(id);
        if (resource is null) throw new NotFoundException("The resource was not found.");
        
        return View(resource);
    }


    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirm(int id)
    {
        var resource = await componentRepository.GetByIdAsync(id);
        if (resource is null) throw new NotFoundException("The resource was not found.");
        
        await componentRepository.DeleteAsync(id);
        
        return RedirectToAction(nameof(Index));
    }
}