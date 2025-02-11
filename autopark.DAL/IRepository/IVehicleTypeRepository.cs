using autopark.DAL.Entities;
using autopark.DAL.IRepository.Base;

namespace autopark.DAL.IRepository;

public interface IVehicleTypeRepository :
    IAllGettable<VehicleType>,
    IGettableById<VehicleType>,
    ICreatable<VehicleType>,
    IUpdatable<VehicleType>,
    IDeletable
{
    
}