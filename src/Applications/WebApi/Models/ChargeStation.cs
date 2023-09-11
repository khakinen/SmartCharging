namespace SmartCharging.Application.WebApi.Models;

public class ChargeStation
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid GroupId { get; set; }
}