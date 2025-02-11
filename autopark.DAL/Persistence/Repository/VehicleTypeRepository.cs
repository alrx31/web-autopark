using System.Data;
using autopark.DAL.Entities;
using autopark.DAL.IRepository;
using Dapper;

namespace autopark.DAL.Persistence.Repository
{
    public class VehicleTypeRepository(IDbConnection dbConnection) : IVehicleTypeRepository
    {
        public async Task<IEnumerable<VehicleType>> GetAllAsync()
        {
            string sql = "SELECT * FROM VehicleTypes";

            return await dbConnection.QueryAsync<VehicleType>(sql);
        }

        public async Task<VehicleType> GetByIdAsync(int vehicleTypeId)
        {
            string sql = "SELECT * FROM VehicleTypes WHERE VehicleTypeId = @VehicleTypeId";

            return (await dbConnection.QuerySingleOrDefaultAsync<VehicleType>(sql, new { VehicleTypeId = vehicleTypeId }))!;
        }

        public async Task<int> AddAsync(VehicleType vehicleType)
        {
            string sql = @"
                    INSERT INTO VehicleTypes (Name, TaxCoefficient)
                    VALUES (@Name, @TaxCoefficient);
                    SELECT CAST(SCOPE_IDENTITY() as int)";

            return await dbConnection.QuerySingleAsync<int>(sql, vehicleType);
        }

        public async Task UpdateAsync(VehicleType vehicleType)
        {
            string sql = @"
                    UPDATE VehicleTypes 
                    SET Name = @Name, 
                        TaxCoefficient = @TaxCoefficient
                    WHERE VehicleTypeId = @VehicleTypeId";

            await dbConnection.ExecuteAsync(sql, vehicleType);
        }

        public async Task DeleteAsync(int vehicleTypeId)
        {
            string sql = "DELETE FROM VehicleTypes WHERE VehicleTypeId = @VehicleTypeId";

            await dbConnection.ExecuteAsync(sql, new { VehicleTypeId = vehicleTypeId });
        }
    }
}