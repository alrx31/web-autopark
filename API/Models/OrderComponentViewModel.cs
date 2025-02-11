using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class OrderComponentViewModel
{
    public required int Id { get; set; }
    public required float Quantity { get; set; }
}