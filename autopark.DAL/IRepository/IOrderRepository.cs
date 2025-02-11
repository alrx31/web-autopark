using autopark.DAL.Entities;
using autopark.DAL.IRepository.Base;

namespace autopark.DAL.IRepository;

public interface IOrderRepository :
    IAllGettable<Order>,
    IGettableById<Order>,
    ICreatable<Order>,
    IDeletable
{
}