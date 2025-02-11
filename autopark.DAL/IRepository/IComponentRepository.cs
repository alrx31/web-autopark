using autopark.DAL.Entities;
using autopark.DAL.IRepository.Base;

namespace autopark.DAL.IRepository;

public interface IComponentRepository : 
    IGettableById<Component>,
    IAllGettable<Component>,
    ICreatable<Component>,
    IUpdatable<Component>,
    IDeletable
{
    
}