namespace SmartCharging.Application.WebApi.Models;

public class CreateGroupRequest
{
    public string Name { get; set; }
    public int CapacityInAmps { get; set; }
}