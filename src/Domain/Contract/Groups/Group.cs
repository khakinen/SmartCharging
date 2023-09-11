namespace SmartCharging.Domain.Contract.Groups;

public class Group
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int CapacityInAmps { get; set; }
}