namespace SmartCharging.Application.WebApi.Models;

public class Group
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
}