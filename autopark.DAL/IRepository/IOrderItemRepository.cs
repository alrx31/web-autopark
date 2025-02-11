using autopark.DAL.Entities;
using autopark.DAL.IRepository.Base;

namespace autopark.DAL.IRepository;

public interface IOrderItemRepository :
    IAllGettable<OrderItems>,
    IGettableById<OrderItems>,
    ICreatable<OrderItems>
{
    
}