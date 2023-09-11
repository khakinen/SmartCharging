namespace SmartCharging.Application.WebApi.Models;

public class UpdateGroupRequest
{
    public string Name { get; set; }
    public int CapacityInAmps { get; set; }
}