using System.Data;
using autopark.DAL.Entities;
using autopark.DAL.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;

namespace autopark.DAL.Persistence.Repository;

public class VehicleRepository(IDbConnection dbConnection) : IVehicleRepository
{
    public async Task<IEnumerable<Vehicle>> GetAllAsync()
    {
        string sql = "SELECT * FROM Vehicle";
        
        return await dbConnection.QueryAsync<Vehicle>(sql);
    }

    public async Task<Vehicle> GetByIdAsync(int vehicleId)
    {
        string sql = "SELECT * FROM Vehicle WHERE VehicleId = @VehicleId";
        
        return (await dbConnection.QueryFirstOrDefaultAsync<Vehicle>(sql, new { VehicleId = vehicleId }))!;
    }

    public async Task<int> AddAsync(Vehicle vehicle)
    {
        string sql = @"
                INSERT INTO Vehicle (VehicleTypeId, Model, RegistrationNumber, Weight, Year, Mileage, Color, FuelConsumption)
                VALUES (@VehicleTypeId, @Model, @RegistrationNumber, @Weight, @Year, @Mileage, @Color, @FuelConsumption);
                SELECT CAST(SCOPE_IDENTITY() as int)";
        
        return await dbConnection.QuerySingleAsync<int>(sql, vehicle);
    }

    public async Task UpdateAsync(Vehicle vehicle)
    {
        string sql = @"
                UPDATE Vehicle
                SET VehicleTypeId = @VehicleTypeId, 
                    Model = @Model, 
                    RegistrationNumber = @RegistrationNumber, 
                    Weight = @Weight, 
                    Year = @Year, 
                    Mileage = @Mileage, 
                    Color = @Color, 
                    FuelConsumption = @FuelConsumption
                WHERE VehicleId = @VehicleId";
        
        await dbConnection.ExecuteAsync(sql, vehicle);
    }

    public async Task DeleteAsync(int vehicleId)
    {
        string sql = "DELETE FROM Vehicle WHERE VehicleId = @VehicleId";
        
        await dbConnection.ExecuteAsync(sql, new { VehicleId = vehicleId });
    }
}