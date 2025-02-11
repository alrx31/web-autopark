using System.Data;
using autopark.DAL.Entities;
using autopark.DAL.IRepository;
using Dapper;

namespace autopark.DAL.Persistence.Repository
{
    public class OrderRepository(IDbConnection dbConnection) : IOrderRepository
    {
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            string sql = @"select 
                    o.OrderId,
                    o.VehicleId,
                    v.Model AS VehicleModel,
                    c.Name AS ComponentName,
                    i.Quantity,
                    o.Date
                    from OrderItems i
                LEFT JOIN dbo.Orders o on i.OrderId = o.OrderId
                LEFT JOIN dbo.Components C on C.ComponentId = i.ComponentId
                LEFT JOIN dbo.Vehicle V on o.VehicleId = V.VehicleId;
                ";

            return await dbConnection.QueryAsync<Order>(sql);
        }

        public async Task<Order> GetByIdAsync(int orderId)
        {
            string sql = @"select 
                o.OrderId,
                o.VehicleId,
                v.Model AS VehicleModel,
                c.Name AS ComponentName,
                i.Quantity,
                o.Date
                from OrderItems i
            LEFT JOIN dbo.Orders o on i.OrderId = o.OrderId
            LEFT JOIN dbo.Components C on C.ComponentId = i.ComponentId
            LEFT JOIN dbo.Vehicle V on o.VehicleId = V.VehicleId
               Where o.OrderId = @OrderId;
            ";

            return (await dbConnection.QuerySingleOrDefaultAsync<Order>(sql, new { OrderId = orderId }))!;
        }

        public async Task<int> AddAsync(Order order)
        {
            string sql = @"
                INSERT INTO Orders (VehicleId, Date)
                VALUES (@VehicleId, @Date);
                SELECT CAST(SCOPE_IDENTITY() as int)";

            return await dbConnection.QuerySingleAsync<int>(sql, order);
        }

        public async Task DeleteAsync(int orderId)
        {
            string sql = "DELETE FROM Orders WHERE OrderId = @OrderId";

            await dbConnection.ExecuteAsync(sql, new { OrderId = orderId });
        }
    }
}