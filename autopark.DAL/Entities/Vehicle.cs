using System.ComponentModel.DataAnnotations;

namespace autopark.DAL.Entities;

public class Vehicle
{
    [Range(0, int.MaxValue)]
    public required int VehicleId { get; set; }
    [Range(0, int.MaxValue)]
    public required int VehicleTypeId { get; init; } 
    [StringLength(maximumLength:100, ErrorMessage = "Model length should be less than100.")]
    public required string Model { get; init; }
    [StringLength(maximumLength:20, ErrorMessage = "Model length should be less than100.")]
    public required string RegistrationNumber { get; init; }
    [Range(0, float.MaxValue)]
    public required float Weight { get; init; }
    [Range(0, short.MaxValue)]
    public required short Year { get; init; }
    [Range(0, float.MaxValue)]
    public required float Mileage { get; init; }
    [StringLength(maximumLength:100, ErrorMessage = "Color length should be less than100.")]
    public required string Color { get; init; }
    [Range(0, float.MaxValue)]
    public required float FuelConsumption { get; init; }
    
    public VehicleType? VehicleType { get; init; }

    public object GetPropertyByCriteria(int criteria)
    {
        return criteria switch
        {
            0 => VehicleId,
            1 => VehicleTypeId,
            2 => Model,
            3 => Color,
            4 => FuelConsumption,
            5 => RegistrationNumber,
            6 => Mileage,
            7 => Weight,
            8 => Year,
            _ => throw new ArgumentException("Invalid Vehicle criteria.")
        };
    }
}