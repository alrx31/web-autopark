using System.Data;
using autopark.DAL.Entities;
using autopark.DAL.IRepository;
using Dapper;

namespace autopark.DAL.Persistence.Repository
{
    public class OrderItemsRepository(IDbConnection dbConnection) : IOrderItemRepository
    {
        public async Task<IEnumerable<OrderItems>> GetAllAsync()
        {
            string sql = "SELECT * FROM OrderItems";

            return await dbConnection.QueryAsync<OrderItems>(sql);
        }

        public async Task<OrderItems> GetByIdAsync(int orderItemId)
        {
            string sql = "SELECT * FROM OrderItems WHERE OrderItemId = @OrderItemId";

            return (await dbConnection.QuerySingleOrDefaultAsync<OrderItems>(sql, new { OrderItemId = orderItemId }))!;
        }

        public async Task<int> AddAsync(OrderItems orderItem)
        {
            string sql = @"
                    INSERT INTO OrderItems (OrderId, ComponentId, Quantity)
                    VALUES (@OrderId, @ComponentId, @Quantity);
                    SELECT CAST(SCOPE_IDENTITY() as int)";

            return await dbConnection.QuerySingleAsync<int>(sql, orderItem);
        }
    }
}