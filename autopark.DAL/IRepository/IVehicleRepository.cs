using autopark.DAL.Entities;
using autopark.DAL.IRepository.Base;

namespace autopark.DAL.IRepository;

public interface IVehicleRepository :
    IAllGettable<Vehicle>,
    IGettableById<Vehicle>,
    ICreatable<Vehicle>,
    IUpdatable<Vehicle>,
    IDeletable
{
    
}