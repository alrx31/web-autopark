using System.ComponentModel.DataAnnotations;

namespace autopark.DAL.Entities;
public class VehicleType
{
    [Range(0,int.MaxValue)]
    public required int VehicleTypeId { get; set; }
    [StringLength(maximumLength:100,ErrorMessage = "Name should be less than 100.")]
    public required string Name { get; set; }
    [Range(0,float.MaxValue)]
    public required float TaxCoefficient { get; set; }
}