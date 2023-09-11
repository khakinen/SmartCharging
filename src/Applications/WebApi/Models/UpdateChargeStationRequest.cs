namespace SmartCharging.Application.WebApi.Models;

public class UpdateChargeStationRequest
{
    public string Name { get; set; }
    public Guid GroupId { get; set; }
}