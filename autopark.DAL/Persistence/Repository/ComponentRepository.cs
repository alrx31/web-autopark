using System.Data;
using autopark.DAL.Entities;
using autopark.DAL.IRepository;
using Dapper;

namespace autopark.DAL.Persistence.Repository
{
    public class ComponentRepository(IDbConnection dbConnection) : IComponentRepository
    {
        public async Task<Component> GetByIdAsync(int componentId)
        {
            string sql = "SELECT * FROM Components WHERE ComponentId = @ComponentId";

            return (await dbConnection.QuerySingleOrDefaultAsync<Component>(sql, new { ComponentId = componentId }))!;
        }
        
        
        public Task<IEnumerable<Component>> GetAllAsync()
        {
            string sql = "SELECT * FROM Components";
            
            return dbConnection.QueryAsync<Component>(sql);
        }

        public async Task<int> AddAsync(Component component)
        {
            string sql = @"
                INSERT INTO Components (Name)
                VALUES (@Name);
                SELECT CAST(SCOPE_IDENTITY() as int)";

            return await dbConnection.QuerySingleAsync<int>(sql, component);
        }

        public async Task UpdateAsync(Component component)
        {
            string sql = @"
                UPDATE Components 
                SET Name = @Name
                WHERE ComponentId = @ComponentId";

            await dbConnection.ExecuteAsync(sql, component);
        }

        public async Task DeleteAsync(int componentId)
        {
            string sql = "DELETE FROM Components WHERE ComponentId = @ComponentId";

            await dbConnection.ExecuteAsync(sql, new { ComponentId = componentId });
        }
    }
}