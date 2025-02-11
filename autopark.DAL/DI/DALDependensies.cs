using System.Data;
using autopark.DAL.Entities;
using autopark.DAL.IRepository;
using autopark.DAL.Persistence.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
// ReSharper disable InconsistentNaming

namespace autopark.DAL.DI;

public static class DALDependencies
{
    public static IServiceCollection AddDALDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDbConnection>(sp =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        });
        
        services.AddScoped<IOrderRepository,OrderRepository>();
        services.AddScoped<IOrderItemRepository,OrderItemsRepository>();
        services.AddScoped<IComponentRepository,ComponentRepository>();
        services.AddScoped<IVehicleRepository,VehicleRepository>();
        services.AddScoped<IVehicleTypeRepository,VehicleTypeRepository>();
        
        return services;
    }
}